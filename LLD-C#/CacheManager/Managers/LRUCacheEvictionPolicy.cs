using CacheManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Managers
{
    internal class LRUCacheEvictionPolicy<TKey> : ICacheEvictionPolicy<TKey>
    {
        private LinkedList<TKey> dll;
        public LRUCacheEvictionPolicy() { 
            dll = new LinkedList<TKey>();
        }

        private void AddToLast(TKey key)
        {
            dll.Remove(key);
            dll.AddLast(key);
        }
        public void AddKey(TKey key)
        {
            dll.AddLast(key);
        }

        public void GetOnKey(TKey key)
        {
            AddToLast(key);
        }

        public TKey KeyToEvict()
        {
            return dll.First.Value;
        }

        public void PutOnKey(TKey key)
        {
            AddToLast(key);
        }

        public void RemoveOnKey(TKey key)
        {
            dll.Remove(dll.Find(key));
        }
    }
}
