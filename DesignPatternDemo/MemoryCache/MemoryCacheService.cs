using System;
using System.Runtime.Caching;

namespace DesignPatternDemo.MemoryCache
{
    public class MemoryCacheService
    {
        private static readonly ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;

        public static object GetCacheValue(string key)
        {
            if (key != null && cache.Contains(key))
            {
                return cache[key];
            }
            return default(object);
        }

        public static void SetCacheValue(string key, object value)
        {
            if (key != null)
            {
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    SlidingExpiration = TimeSpan.FromHours(1)
                };
                cache.Set(key, value, policy);
            }
        }
    }
}