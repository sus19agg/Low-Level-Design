using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Interfaces
{
    internal interface IMessageBroker
    {
        void AddNewTopic(string topic);
        void DeleteTopic(string topic);
        void AddSubscriberToTopic(string topic, ISubscriber subscriber);
        void AddSubscribersToTopic(string topic, List<ISubscriber> subscribers);
        void RemoveSubscriberFromTopic(string topic, ISubscriber subscriber);
        void PublishMessageToTopic(string topic, string id, string message, Object? AdditionalProperties);
        void ResetTopic(string topic);
        void ProcessDeadLetterQueue(string topic);
    }
}
