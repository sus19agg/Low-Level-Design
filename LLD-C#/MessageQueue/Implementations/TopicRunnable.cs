using MessageBroker.Interfaces;
using MessageBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBroker.Implementations
{
    internal class TopicRunnable : ITopicRunnable
    {
        private readonly ITopic topic;
        public TopicRunnable(ITopic topic)
        {
            this.topic = topic;
        }
        public void Run()
        {
            try
            {
                while (topic != null && topic.IsTopicActive())
                {
                    QueueMessage? queueMessage = topic.GetNextMessage();
                    if (queueMessage != null)
                    {
                        topic.NotifySubscribers(queueMessage);
                    }
                    else
                    {
                        Thread.Sleep(100); // Sleep for a short duration to avoid busy waiting
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
