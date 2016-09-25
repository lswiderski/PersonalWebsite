using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace PersonalWebsite.Common
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private static List<string> keys;

        public static List<string> Keys
        {
            get
            {
                if (keys == null)
                {
                    keys = new List<string>();
                }
                return keys;
            }
        }

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
            Keys.Add(key);
        }

        public string Store<T>(T obj) where T : class
        {
            var key = new Guid().ToString();
            _memoryCache.Set(key, obj,
                  new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromMinutes(LifeTime)));
            Keys.Add(key);
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

        public void Clear()
        {
            foreach (var key in Keys)
            {
                _memoryCache.Remove(key);
            }
            Keys.Clear();
        }
    }
}