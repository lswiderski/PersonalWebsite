using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services
{
    public interface ICacheService
    {
        void Store<T>(string key, T obj) where T : class;

        string Store<T>(T obj) where T : class;

        T Get<T>(string key) where T : class;

        bool Get<T>(string key, out T output) where T : class;

        bool IsCached(string key);

        void Clear();
    }
}
