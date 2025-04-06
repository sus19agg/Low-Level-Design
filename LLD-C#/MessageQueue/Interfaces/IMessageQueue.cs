using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Interfaces
{
    internal interface IMessageQueue<T>
    {
        bool HasMessage();
        bool GetNextMessage(out T? message);
        void AddMessage(T message);
        void Clear();
    }
}
