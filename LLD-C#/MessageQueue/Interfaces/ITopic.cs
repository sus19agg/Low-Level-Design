using MessageBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Interfaces
{
    internal interface ITopic
    {
        void PublishMessage(QueueMessage queueMessage);
        void AddSubscriber(ISubscriber subscriber);
        void RemoveSubscriber(ISubscriber subscriber);
        bool IsTopicActive();
        void NotifySubscribers(QueueMessage queueMessage);
        QueueMessage? GetNextMessage();
        void ResetTopic();
        void PrintDeadLetterQueue();
    }
}
