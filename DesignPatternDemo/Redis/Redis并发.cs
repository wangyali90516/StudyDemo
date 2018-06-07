using System;
using System.Threading;
using System.Threading.Tasks;
using DesignPatternDemo.Redis.并发;

namespace DesignPatternDemo.Redis
{
    public class Redis并发
    {
        public static void Main()
        {
            //不处理并发时，会出现某个方法没执行完，另外的方法就已经进来，容易导致数据错乱
            //ParallelLoopResult result2 = Parallel.For(0, 10, i =>
            //{
            //    Thread.Sleep(200);
            //    Console.WriteLine(i + "已进入");
            //    Thread.Sleep(200);
            //    Console.WriteLine(i + "已完成");
            //});
            //Console.WriteLine("是否完成:{0}", result2.IsCompleted);
            //Console.ReadKey();

            //处理并发方式，则会保证某一个线程处理完成之后，另外的线程才能进去。保证了数据的可用性
            ParallelLoopResult result = Parallel.For(0, 10, i =>
            {
                ISynchronizer synchronizer = new RedisSynchronizer(ConfigManager.GetRedisClient());
                string redisKey = "TestAsync";
                synchronizer.DeadLockSeconds = 3;
                BaseSyncLocker locker;
                if (synchronizer.TryGetSyncLock(redisKey, out locker))
                {
                    using (locker)
                    {
                        Console.WriteLine(i + "已进入");
                        Thread.Sleep(200);
                        Console.WriteLine(i + "已完成");
                    }
                }
            });
            Console.WriteLine("是否完成:{0}", result.IsCompleted);
            Console.ReadKey();
        }
    }
}