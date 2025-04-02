using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Interfaces
{
    internal interface ICacheStore<TKey, TValue>
    {
        int capacity {  get; }
        bool DoesKeyExist(TKey key);
        TValue Get(TKey key);
        void Put(TKey key, TValue val);
        void Remove(TKey key);
        int Count();
    }
}
