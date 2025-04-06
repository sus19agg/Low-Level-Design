using MessageBroker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Implementations
{
    internal class FIFOMessageQueue<T> : IMessageQueue<T>
    {
        Queue<T> queue;

        public FIFOMessageQueue()
        {
            queue = new Queue<T>();
        }

        public void AddMessage(T message)
        {
            lock(queue)
            {
                queue.Enqueue(message);
            }
        }

        public void Clear()
        {
            queue.Clear();
        }

        public bool GetNextMessage(out T? message)
        {
            lock (queue)
            {
                return queue.TryDequeue(out message);
            }
        }

        public bool HasMessage()
        {
            lock (queue)
            {
                return queue.Count != 0;
            }
        }
    }
}
