using MessageBroker.Interfaces;
using MessageBroker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Implementations
{
    internal class PushBasedMessageBroker : IMessageBroker
    {
        private readonly Dictionary<string, ITopic> topics;
        private static IMessageBroker instance;
        private static readonly object lockObj = new object();

        private PushBasedMessageBroker() { 
            topics = new Dictionary<string, ITopic>();
        }

        public static IMessageBroker GetMessageBroker()
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new PushBasedMessageBroker();
                    }
                }
            }
            return instance;
        }

        public void AddNewTopic(string topic)
        {
            if (!topics.ContainsKey(topic))
            {
                topics.Add(topic, new Topic(topic));
            }
        }

        public void AddSubscriberToTopic(string topic, ISubscriber subscriber)
        {
            if (topics.ContainsKey(topic)) {
                topics[topic].AddSubscriber(subscriber);
            }
            else
            {
                throw new ArgumentException($"Topic {topic} does not exist");
            }
        }

        public void AddSubscribersToTopic(string topic, List<ISubscriber> subscribers)
        {
            if (topics.ContainsKey(topic))
            {
                foreach (var subscriber in subscribers)
                {
                    topics[topic].AddSubscriber(subscriber);
                }
            }
            else
            {
                throw new ArgumentException($"Topic {topic} does not exist");
            }
        }

        public void DeleteTopic(string topic)
        {
            if (!topics.ContainsKey(topic))
            {
                throw new ArgumentException($"Topic {topic} does not exist");
            }
            topics[topic].ResetTopic();
            topics.Remove(topic);
        }

        public void PublishMessageToTopic(string topic, string id, string message, object? additionalProperties)
        {
            if (topics.ContainsKey(topic))
            {
                QueueMessage queueMessage = new QueueMessage() { Id = id, Message = message, AdditionalProperties = additionalProperties };
                topics[topic].PublishMessage(queueMessage);
            }
            else
            {
                throw new ArgumentException($"Topic {topic} does not exist");
            }
        }

        public void RemoveSubscriberFromTopic(string topic, ISubscriber subscriber)
        {
            if (topics.ContainsKey(topic))
            {
                topics[topic].RemoveSubscriber(subscriber);
            }
            else
            {
                throw new ArgumentException($"Topic {topic} does not exist");   
            }
        }

        public void ResetTopic(string topic)
        {
            if (topics.ContainsKey(topic))
            {
                topics[topic].ResetTopic();
            }
            else
            {
                throw new ArgumentException($"Topic {topic} does not exist");
            }
        }

        public void ProcessDeadLetterQueue(string topic)
        {
            if(topics.ContainsKey(topic))
            {
                topics[topic].PrintDeadLetterQueue();
            }
            else
            {
                throw new ArgumentException($"Topic {topic} does not exist");
            }
        }
    }
}
