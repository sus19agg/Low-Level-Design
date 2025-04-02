using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Interfaces
{
    internal interface ICacheEvictionPolicy<Tkey>
    {
        void GetOnKey(Tkey key);
        void AddKey(Tkey key);
        void PutOnKey(Tkey key);
        void RemoveOnKey(Tkey key);
        Tkey KeyToEvict();
    }
}
