using CacheManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Managers
{
    internal class LFUCacheEvictionPolicy<TKey> : ICacheEvictionPolicy<TKey>
    {
        private Dictionary<TKey, int> KeyToFrequency;
        private SortedDictionary<int, LinkedList<TKey>> FreqToDLL;

        public LFUCacheEvictionPolicy()
        {
            KeyToFrequency = new Dictionary<TKey, int>();
            FreqToDLL = new SortedDictionary<int, LinkedList<TKey>>();
        }

        private void AddToLast(TKey key)
        {
            int freq = KeyToFrequency[key];
            FreqToDLL[freq].Remove(key);
            if(FreqToDLL[freq].Count==0)
            {
                FreqToDLL.Remove(freq);
            }
            int newFreq = freq + 1;
            if(!FreqToDLL.ContainsKey(newFreq))
            {
                FreqToDLL[newFreq] = new LinkedList<TKey>();
            }
            FreqToDLL[newFreq].AddLast(key);
            KeyToFrequency[key] = newFreq;
        }
        public void AddKey(TKey key)
        {
            KeyToFrequency[key] = 1;
            if (!FreqToDLL.ContainsKey(1))
            {
                FreqToDLL[1] = new LinkedList<TKey>();
            }
            FreqToDLL[1].AddLast(key);
        }

        public void GetOnKey(TKey key)
        {
            AddToLast(key);
        }

        public TKey KeyToEvict()
        {
            return FreqToDLL.First().Value.First();
        }

        public void PutOnKey(TKey key)
        {
            AddToLast(key);
        }

        public void RemoveOnKey(TKey key)
        {
            int freq = KeyToFrequency[key];
            FreqToDLL[freq].Remove(key);
            if (FreqToDLL[freq].Count == 0)
            {
                FreqToDLL.Remove(freq);
            }
        }
    }
}
