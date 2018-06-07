using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;
using Moe.Lib;
using StackExchange.Redis;

namespace DesignPatternDemo.Redis.并发
{
    /// <summary>
    ///     同步器接口
    /// </summary>
    public interface ISynchronizer
    {
        /// <summary>
        ///     认定死锁的秒数
        /// </summary>
        int DeadLockSeconds { get; set; }

        /// <summary>
        ///     重试的延迟间隔
        /// </summary>
        int DelayInterval { get; set; }

        /// <summary>
        ///     最大重试次数
        /// </summary>
        int MaxRepeatCount { get; set; }

        /// <summary>
        ///     尝试获取同步锁，获取成功成返回true，超时则返回false
        /// </summary>
        /// <param name="syncKey">同步锁key值</param>
        /// <param name="syncLock">同步锁</param>
        /// <returns>是否获取成功</returns>
        bool TryGetSyncLock(string syncKey, out BaseSyncLocker syncLock);
    }

    /// <summary>
    ///     同步锁基类
    /// </summary>
    public abstract class BaseSyncLocker : IDisposable
    {
        protected bool _isRelease;

        #region IDisposable Members

        public void Dispose()
        {
            this.ReleaseSyncLock();
        }

        #endregion IDisposable Members

        public void Release()
        {
            if (!this._isRelease)
            {
                this.ReleaseSyncLock();
                this._isRelease = true;
            }
        }

        protected abstract void ReleaseSyncLock();
    }

    /// <summary>
    ///     同步器的Redis实现
    /// </summary>
    public class RedisSynchronizer : ISynchronizer
    {
        private readonly IDatabase redisDb; // = ConfigManager.GetMemberRedisClient();

        public RedisSynchronizer(IDatabase redisDb)
        {
            this.redisDb = redisDb;
        }

        #region ISynchronizer Members

        /// <summary>
        ///     认定死锁的秒数
        /// </summary>
        public int DeadLockSeconds { get; set; } = 6;

        /// <summary>
        ///     重试的延迟间隔
        /// </summary>
        public int DelayInterval { get; set; } = 100;

        /// <summary>
        ///     最大重试次数
        /// </summary>
        public int MaxRepeatCount { get; set; } = 11;

        public bool TryGetSyncLock(string syncKey, out BaseSyncLocker syncLock)
        {
            int executeCount = 0;
            while (executeCount < this.MaxRepeatCount)
            {
                try
                {
                    //尝试获取同步锁
                    if (!this.redisDb.StringSet(syncKey, DateTime.Now.ToString(CultureInfo.InvariantCulture), null, When.NotExists))
                    {
                        //未获取到锁，进行等待操作，此处做了10毫秒延迟
                        Task.Delay(TimeSpan.FromMilliseconds(this.DelayInterval)).Wait();
                        //执行次数+1
                        executeCount++;
                        if (executeCount % 5 == 0)
                        {
                            //每五次执行时，检测一次是否出现死锁
                            if (this.CheckIsDeadLock(syncKey))
                            {
                                //如果出现死锁，尝试释放该锁
                                if (this.ReleaseSyncLock(syncKey))
                                {
                                    //释放死锁
                                }
                            }
                        }

                        continue;
                    }
                }
                catch
                {
                    Task.Delay(TimeSpan.FromMilliseconds(this.DelayInterval)).Wait();
                    //执行次数+1
                    executeCount++;
                    continue;
                }

                syncLock = new RedisSyncLocker(this, syncKey);
                return true;
            }

            syncLock = null;
            return false;
        }

        #endregion ISynchronizer Members

        public bool ReleaseSyncLock(string syncKey)
        {
            return this.ReleaseDeadLock(syncKey);
        }

        /// <summary>
        ///     检查是否是死锁
        /// </summary>
        /// <param name="syncKey">同步锁Key</param>
        /// <returns>是否为死锁</returns>
        private bool CheckIsDeadLock(string syncKey)
        {
            try
            {
                if (this.redisDb.KeyExists(syncKey))
                {
                    RedisValue syncTimeReidsValue = this.redisDb.StringGet(syncKey);
                    DateTime now = DateTime.Now;
                    DateTime syncTime = syncTimeReidsValue.ToString().ToDateTime();
                    return now >= syncTime.AddSeconds(this.DeadLockSeconds);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     检查是否子死锁
        /// </summary>
        /// <param name="syncKey">同步锁Key</param>
        /// <param name="deep">深度</param>
        /// <returns>是否为死锁</returns>
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        private bool CheckIsSubDeadLock(string syncKey, int deep)
        {
            try
            {
                string tempKey = syncKey + deep;
                if (this.redisDb.KeyExists(tempKey))
                {
                    RedisValue syncTimeReidsValue = this.redisDb.StringGet(tempKey);
                    DateTime now = DateTime.Now;
                    DateTime syncTime = syncTimeReidsValue.ToString().ToDateTime();
                    return now >= syncTime.AddSeconds(this.DeadLockSeconds / 2);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     获取子同步锁
        /// </summary>
        /// <param name="syncKey">同步锁的key值</param>
        /// <param name="deep">深度</param>
        /// <returns>是否获取成功</returns>
        private bool GetSubSyncLock(string syncKey, int deep)
        {
            return this.redisDb.StringSet(syncKey + deep, DateTime.UtcNow.ToChinaStandardTime().ToString(CultureInfo.InvariantCulture), null, When.NotExists);
        }

        /// <summary>
        ///     释放死锁
        /// </summary>
        /// <param name="syncKey">同步锁的key值</param>
        /// <returns>System.Boolean.</returns>
        private bool ReleaseDeadLock(string syncKey)
        {
            if (this.GetSubSyncLock(syncKey, 1))
            {
                this.redisDb.KeyDelete(syncKey);
                this.ReleaseSubLock(syncKey, 1);
                return true;
            }

            if (this.CheckIsSubDeadLock(syncKey, 1))
            {
                if (this.ReleaseSubDeadLock(syncKey, 1))
                {
                    this.redisDb.KeyDelete(syncKey);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     释放子死锁
        /// </summary>
        /// <param name="syncKey">同步锁Key</param>
        /// <param name="deep">深度</param>
        /// <returns>是否释放死锁</returns>
        private bool ReleaseSubDeadLock(string syncKey, int deep)
        {
            if (this.GetSubSyncLock(syncKey, deep + 1))
            {
                this.redisDb.KeyDelete(syncKey + deep);
                this.ReleaseSubLock(syncKey, deep + 1);
                return true;
            }

            if (this.CheckIsSubDeadLock(syncKey, deep + 1))
            {
                if (this.ReleaseSubDeadLock(syncKey, deep + 1))
                {
                    this.redisDb.KeyDelete(syncKey + deep);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     释放子同步锁
        /// </summary>
        /// <param name="syncKey">同步锁的key值</param>
        /// <param name="deep">深度</param>
        private void ReleaseSubLock(string syncKey, int deep)
        {
            this.redisDb.KeyDelete(syncKey + deep);
        }

        #region Nested type: RedisSyncLocker

        /// <summary>
        ///     Redis同步锁的实现类
        /// </summary>
        public class RedisSyncLocker : BaseSyncLocker
        {
            public RedisSyncLocker(RedisSynchronizer synchronizer, string syncKey)
            {
                this.Synchronizer = synchronizer;
                this.SyncKey = syncKey;
            }

            public RedisSynchronizer Synchronizer { get; }
            public string SyncKey { get; }

            protected override void ReleaseSyncLock()
            {
                this.Synchronizer.ReleaseSyncLock(this.SyncKey);
            }
        }

        #endregion Nested type: RedisSyncLocker
    }
}