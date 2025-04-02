using CacheManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Interfaces
{
    internal interface ICacheEvictionPolicyFactory<TKey>
    {
        ICacheEvictionPolicy<TKey> CreateCacheEvictionPolicy(PolicyType policyType);
    }
}
