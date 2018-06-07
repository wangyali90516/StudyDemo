using DesignPatternDemo.quartz;
using StackExchange.Redis;

namespace DesignPatternDemo.Redis.并发
{
    public class ConfigManager
    {
        /// <summary>
        ///     锁
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        ///     redis 连接对象
        /// </summary>
        private static IConnectionMultiplexer connMultiplexer;

        /// <summary>
        ///     获取 Redis 连接对象
        /// </summary>
        /// <returns></returns>
        public static IConnectionMultiplexer ConnectionRedisMultiplexer
        {
            get
            {
                if (connMultiplexer == null || !connMultiplexer.IsConnected)
                {
                    lock (Locker)
                    {
                        if (connMultiplexer == null || !connMultiplexer.IsConnected)
                        {
                            ConfigurationOptions options = GetConfigurationOptions(Settings.RedisConnectionString);
                            connMultiplexer = ConnectionMultiplexer.Connect(options);
                        }
                    }
                }

                return connMultiplexer;
            }
        }

        public static IDatabase GetRedisClient()
        {
            return ConnectionRedisMultiplexer.GetDatabase(Settings.RedisDb, new object());
        }

        /// <summary>
        ///     redis初始化
        /// </summary>
        /// <param name="bizRedisConnectionString"></param>
        /// <returns></returns>
        private static ConfigurationOptions GetConfigurationOptions(string bizRedisConnectionString)
        {
            ConfigurationOptions options = ConfigurationOptions.Parse(bizRedisConnectionString, true);
            options.AbortOnConnectFail = false;
            options.AllowAdmin = true;
            options.ConnectRetry = 10;
            options.ConnectTimeout = 20000;
            options.DefaultDatabase = 0;
            options.ResponseTimeout = 20000;
            options.Ssl = false;
            options.SyncTimeout = 20000;
            return options;
        }
    }
}