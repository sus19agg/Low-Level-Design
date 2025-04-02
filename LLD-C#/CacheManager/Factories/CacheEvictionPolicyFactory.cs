using CacheManager.Enums;
using CacheManager.Interfaces;
using CacheManager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Factories
{
    internal class CacheEvictionPolicyFactory<TKey> : ICacheEvictionPolicyFactory<TKey>
    {
        public ICacheEvictionPolicy<TKey> CreateCacheEvictionPolicy(PolicyType policyType)
        {
            switch(policyType) 
            {
                case PolicyType.LRU:
                    return new LRUCacheEvictionPolicy<TKey>();
                case PolicyType.LFU:
                    return new LFUCacheEvictionPolicy<TKey>();
                default:
                    throw new NotSupportedException("Invalid eviction policy type");
            }
        }
    }
}
