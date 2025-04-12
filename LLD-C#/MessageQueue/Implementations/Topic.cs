using MessageBroker.Interfaces;
using MessageBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Implementations
{
    internal class Topic : ITopic
    {
        private readonly string Name;
        private readonly List<ISubscriber> subscribers;
        private readonly IMessageQueue<QueueMessage> messageQueue;
        private readonly IMessageQueue<QueueMessage> deadLetterQueue;
        private bool active;
        private Thread thread;

        private void ActivateTopic()
        {
            active = true;
            ITopicRunnable topicRunnable = new TopicRunnable(this);
            thread = new Thread(topicRunnable.Run);
            thread.Start();
        }

        public Topic(string name)
        {
            Name = name;
            subscribers = new List<ISubscriber>();
            messageQueue = new FIFOMessageQueue<QueueMessage>();
            deadLetterQueue = new FIFOMessageQueue<QueueMessage>();
            this.ActivateTopic();
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            lock (subscribers) { 
                subscribers.Add(subscriber);
            }
        }

        public void PublishMessage(QueueMessage queueMessage)
        {
            if (active == false) { 
                ActivateTopic();
            }
            messageQueue.AddMessage(queueMessage);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            lock (subscribers) {
                if (subscribers.Contains(subscriber))
                {
                    subscribers.Remove(subscriber);
                }
                else
                {
                    throw new ArgumentException($"Subscriber {subscriber.Name} is not subscribed to topic {Name}");
                }
            }
        }

        public void NotifySubscribers(QueueMessage queueMessage) 
        {
            lock (subscribers)
            {
                foreach (var subscriber in subscribers)
                {
                    try
                    {
                        subscriber.ReceiveMessage(Name, queueMessage.Id, queueMessage.Message, queueMessage.AdditionalProperties);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error notifying subscriber {subscriber.Name}: {ex.Message}");
                        // Optionally, you can add the message to a dead letter queue
                        deadLetterQueue.AddMessage(queueMessage);
                    }
                }
            }
        }

        public bool IsTopicActive()
        {
            return active;
        }

        public QueueMessage? GetNextMessage()
        {
            if(this.messageQueue.HasMessage())
            {
                this.messageQueue.GetNextMessage(out QueueMessage? message);
                return message;
            }
            else
            {
                return null;
            }
        }

        public void ResetTopic()
        {
            active = false;
            thread.Join();
            this.subscribers.Clear();
            this.messageQueue.Clear();
            this.deadLetterQueue.Clear();
        }

        public void PrintDeadLetterQueue()
        {
            Console.WriteLine($"Dead Letter Queue for topic {Name}:");
            while (deadLetterQueue.HasMessage())
            {
                deadLetterQueue.GetNextMessage(out QueueMessage? message);
                if (message != null)
                {
                    Console.WriteLine($"Id: {message.Id}, Message: {message.Message}, Additional Properties: {message.AdditionalProperties}");
                }
            }
        }
    }
}
