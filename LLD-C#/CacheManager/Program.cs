using CacheManager.Enums;
using CacheManager.Factories;
using CacheManager.Interfaces;
using CacheManager.Managers;

namespace CacheManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int capacity = 3;
            ICacheStore<string, int> cacheStore = new DictionaryCacheStore<string, int>(capacity);
            ICacheEvictionPolicyFactory<string> evictionPolicyFactory = new CacheEvictionPolicyFactory<string>();
            ICacheEvictionPolicy<string> cacheEvictionPolicy = evictionPolicyFactory.CreateCacheEvictionPolicy(PolicyType.LRU);

            ICacheManager<string,int> cacheManager = ThreadSafeCacheManager<string,int>.GetInstance(cacheStore, cacheEvictionPolicy);

            cacheManager.PutKey("sush", 1);
            cacheManager.PutKey("sagar", 2);
            Console.WriteLine(cacheManager.GetKey("sagar"));
            Console.WriteLine(cacheManager.GetKey("sush"));
            cacheManager.PutKey("sagar", 1);
            cacheManager.PutKey("sush", 2);
            Console.WriteLine(cacheManager.GetKey("sagar"));
            Console.WriteLine(cacheManager.GetKey("sush"));
            cacheManager.PutKey("vibh", 3);
            Console.WriteLine(cacheManager.GetKey("sush"));
            Console.WriteLine(cacheManager.GetKey("vibh"));
            cacheManager.PutKey("sagar", 2);
            Console.WriteLine(cacheManager.GetKey("sush"));
        }
    }
}
