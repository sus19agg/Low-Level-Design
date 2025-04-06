using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Interfaces
{
    internal interface ISubscriber
    {
        string Name { get; }
        List<String> Topics { get; }
        void ReceiveMessage(string topic, string id, string message, Object? additionalProperties);
    }
}
