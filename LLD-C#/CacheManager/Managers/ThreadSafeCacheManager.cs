using CacheManager.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Managers
{
    internal class ThreadSafeCacheManager<TKey, TValue> : ICacheManager<TKey, TValue>
    {
        private ICacheStore<TKey, TValue> _store;
        private ICacheEvictionPolicy<TKey> _evictionPolicy;
        private int capacity;

        private static ThreadSafeCacheManager<TKey, TValue> _instance = null;

        private ThreadSafeCacheManager(ICacheStore<TKey, TValue> store, ICacheEvictionPolicy<TKey> evictionPolicy)
        {
            this._store = store;
            this._evictionPolicy = evictionPolicy;
            this.capacity = store.capacity;
        }


        private static readonly object lockObject = new object();

        public static ICacheManager<TKey, TValue> GetInstance(ICacheStore<TKey,TValue> store, ICacheEvictionPolicy<TKey> evictionPolicy)
        {
            if(_instance == null)
            {
                lock (lockObject)
                {
                    if(_instance == null)
                    {
                        _instance = new ThreadSafeCacheManager<TKey, TValue>(store, evictionPolicy);
                    }
                }
            }
            return _instance;
        }

        public TValue GetKey(TKey key)
        {
            lock (lockObject)
            {
                TValue value = this._store.Get(key);
                this._evictionPolicy.GetOnKey(key);
                return value;
            }
        }

        public void PutKey(TKey key, TValue val)
        {
            lock (lockObject)
            {
                if (this._store.DoesKeyExist(key))
                {
                    this._store.Put(key, val);
                    this._evictionPolicy.PutOnKey(key);
                }
                else
                {
                    if (this._store.Count() >= this.capacity)
                    {
                        TKey keyToEvict = this._evictionPolicy.KeyToEvict();
                        Console.WriteLine("KeyToEvict: " + keyToEvict);
                        this.RemoveKey(keyToEvict);
                    }
                    this._store.Put(key, val);
                    this._evictionPolicy.AddKey(key);
                }
            }
        }

        public void RemoveKey(TKey key)
        {
            lock (lockObject) 
            {
                this._store.Remove(key);
                this._evictionPolicy.RemoveOnKey(key);
            }
        }
    }
}
