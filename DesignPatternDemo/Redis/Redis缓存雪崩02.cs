using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DesignPatternDemo.Common;

namespace DesignPatternDemo.Redis
{
    internal class Redis缓存雪崩02
    {
        public static object GetProductListNew()
        {
            const int cacheTime = 30;
            const string cacheKey = "product_list";
            //缓存标记。
            const string cacheSign = cacheKey + "_sign";

            var sign = RedisCacheHelper.Get<string>(cacheSign);
            //获取缓存值
            var cacheValue = RedisCacheHelper.Get<string>(cacheKey);
            if (sign != null)
            {
                return cacheValue; //未过期，直接返回。
            }
            else
            {
                RedisCacheHelper.Add(cacheSign, "1", DateTime.Now.AddSeconds(cacheTime));
                ThreadPool.QueueUserWorkItem((arg) =>
                {
                    cacheValue = "2222"; //这里一般是 sql查询数据。
                    RedisCacheHelper.Add(cacheKey, cacheValue, DateTime.Now.AddSeconds(cacheTime * 2)); //日期设缓存时间的2倍，用于脏读。
                });

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