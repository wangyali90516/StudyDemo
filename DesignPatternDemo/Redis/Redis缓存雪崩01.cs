using System;
using DesignPatternDemo.Common;

namespace DesignPatternDemo
{
    /// <summary>
    /// 缓存雪崩是由于原有缓存失效(过期)，新缓存未到期间。所有请求都去查询数据库，而对数据库CPU和内存造成巨大压力，严重的会造成数据库宕机。从而形成一系列连锁反应，造成整个系统崩溃。
    //1. 碰到这种情况，一般并发量不是特别多的时候，使用最多的解决方案是加锁排队
    /// </summary>
    internal class Redis缓存雪崩
    {
        public static object GetProductListNew()
        {
            const int cacheTime = 2;
            const string cacheKey = "product_list";
            const string lockKey = cacheKey;

            var cacheValue = RedisCacheHelper.Get<string>(cacheKey);
            if (cacheValue != null)
            {
                // Console.WriteLine("从Redis中读取");
                return cacheValue;
            }
            else
            {
                lock (lockKey)
                {
                    cacheValue = RedisCacheHelper.Get<string>(cacheKey);
                    if (cacheValue != null)
                    {
                        return cacheValue;
                    }
                    else
                    {
                        cacheValue = "1111"; //这里一般是 sql查询数据。
                        Console.WriteLine("从数据库中读取");
                        RedisCacheHelper.Add(cacheKey, cacheValue, DateTime.Now.AddSeconds(cacheTime));
                    }
                }
                return cacheValue;
            }
        }

        //private static void Main(string[] args)
        //{
        //    Console.WriteLine("缓存雪崩");

        //    Parallel.For(0, 10000, (i, status) => GetProductListNew());

        //    Console.ReadKey();
        //}
    }
}