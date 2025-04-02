using CacheManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Managers
{
    internal class DictionaryCacheStore<TKey, TValue> : ICacheStore<TKey, TValue>
    {
        private Dictionary<TKey, TValue> cacheDictionary;
        public int capacity { get; }

        public DictionaryCacheStore(int capacity)
        {
            this.cacheDictionary = new Dictionary<TKey, TValue>(capacity);
            this.capacity = capacity;
        }

        public bool DoesKeyExist(TKey key)
        {
            return this.cacheDictionary.ContainsKey(key);
        }

        public TValue Get(TKey key)
        {
            if (!DoesKeyExist(key))
            {
                throw new KeyNotFoundException("Key not found");
            }
            return cacheDictionary.GetValueOrDefault(key);
        }

        public void Put(TKey key, TValue val)
        {
            if(!DoesKeyExist(key) && this.cacheDictionary.Count >= this.capacity)
            {
                throw new OutOfMemoryException("Capacity exceeded");
            }
            this.cacheDictionary[key] = val;
        }

        public void Remove(TKey key)
        {
            if (!DoesKeyExist(key))
            {
                throw new KeyNotFoundException("Key not found");
            }
            this.cacheDictionary.Remove(key);
        }

        public int Count()
        {
            return this.cacheDictionary.Count;
        }
    }
}
