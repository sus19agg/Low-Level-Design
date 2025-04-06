using MessageBroker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Implementations
{
    internal class Subscriber : ISubscriber
    {
        public string Name { get; }

        public List<string> Topics { get; }

        public Subscriber(string name)
        {
            Name = name;
            Topics = new List<string>();
        }

        public void ReceiveMessage(string topic, string id, string message, object? additionalProperties)
        {
            if (id == "11" || id == "12")
            {
                throw new ArgumentException($"Subscriber {Name} cannot receive message with id {id}");
            }
            Console.WriteLine($"Subscriber <{Name}> received message from topic {topic}: {message} with properties {additionalProperties?.ToString()}");
        }
    }
}
