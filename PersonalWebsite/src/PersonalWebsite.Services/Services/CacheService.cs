using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using NLog;

namespace PersonalWebsite.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private static List<string> keys;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
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
        public static int LifeTime = 24;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Store<T>(string key, T obj) where T : class
        {
            _memoryCache.Set(key, obj,
                  new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromHours(LifeTime)));
            AddKey(key);
        }

        public string Store<T>(T obj) where T : class
        {
            var key = new Guid().ToString();
            _memoryCache.Set(key, obj,
                  new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromHours(LifeTime)));
            AddKey(key);
            return key;
        }

        public T Get<T>(string key) where T : class
        {
            var fromCache = _memoryCache.Get(key) as T;

            return fromCache;
        }

        public bool Get<T>(string key, out T output) where T : class
        {
            output = _memoryCache.Get(key) as T;
            if (output != null)
            {
                return true;
            }
            return false;
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
            _logger.Info("Cache Keys:" + Keys.ToString());
            foreach (var key in Keys)
            {
                _memoryCache.Remove(key);
            }
            _logger.Info("Cache cleared");
            Keys.Clear();
        }

        private void AddKey(string key)
        {
            _logger.Info("new Cache Key: " + key);
            Keys.Add(key);
        }
    }
}