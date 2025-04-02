using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Interfaces
{
    internal interface ICacheManager<TKey, TValue>
    {
        static abstract ICacheManager<TKey, TValue> GetInstance(ICacheStore<TKey, TValue> store, ICacheEvictionPolicy<TKey> evictionPolicy);
        TValue GetKey(TKey key);
        void PutKey(TKey key, TValue val);
        void RemoveKey(TKey key);
    }
}
