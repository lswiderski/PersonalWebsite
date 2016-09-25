using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace PersonalWebsite.Common
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        // object life time in cache in minutes
        public static int LifeTime = 5;
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Store<T>(string key, T obj) where T : class
        {
            _memoryCache.Set(key, obj,
                  new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromMinutes(LifeTime)));
        }

        public string Store<T>(T obj) where T : class
        {
            var key = new Guid().ToString();
            _memoryCache.Set(key, obj,
                  new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromMinutes(LifeTime)));

            return key;
        }

        public T Get<T>(string key) where T : class
        {
            var fromCache = _memoryCache.Get(key) as T;

            return fromCache;
        }

        public bool IsCached(string key)
        {
            var fromCache = _memoryCache.Get(key);
            if (fromCache != null)
            {
                return true;
            }
            return false;
        }

        
    }
}
