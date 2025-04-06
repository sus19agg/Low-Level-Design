using MessageBroker.Interfaces;
using MessageBroker.Implementations;

namespace MessageQueue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                IMessageBroker messageBroker = PushBasedMessageBroker.GetMessageBroker();

                messageBroker.AddNewTopic("Topic1");
                messageBroker.AddNewTopic("Topic2");
                messageBroker.AddNewTopic("Topic3");

                Subscriber s1 = new Subscriber("s1");
                Subscriber s2 = new Subscriber("s2");
                Subscriber s3 = new Subscriber("s3");
                Subscriber s4 = new Subscriber("s4");
                Subscriber s5 = new Subscriber("s5");

                messageBroker.AddSubscribersToTopic("Topic1", new List<ISubscriber> { s1, s2 });
                messageBroker.AddSubscribersToTopic("Topic2", new List<ISubscriber> { s2, s3 });
                messageBroker.AddSubscribersToTopic("Topic3", new List<ISubscriber> { s4, s5 });

                messageBroker.PublishMessageToTopic("Topic1", "1", "this is message 1", new object());
                messageBroker.PublishMessageToTopic("Topic2", "2", "this is message 2", new object());
                messageBroker.PublishMessageToTopic("Topic3", "3", "this is message 3", new object());
                messageBroker.PublishMessageToTopic("Topic1", "4", "this is message 4", new object());
                messageBroker.PublishMessageToTopic("Topic2", "5", "this is message 5", new object());
                messageBroker.PublishMessageToTopic("Topic3", "6", "this is message 6", new object());
                messageBroker.PublishMessageToTopic("Topic1", "7", "this is message 7", new object());
                messageBroker.PublishMessageToTopic("Topic2", "8", "this is message 8", new object());
                messageBroker.PublishMessageToTopic("Topic3", "9", "this is message 9", new object());

                Thread.Sleep(2000);

                messageBroker.ResetTopic("Topic3");
                messageBroker.AddSubscribersToTopic("Topic3", new List<ISubscriber> { s1, s2 });
                messageBroker.PublishMessageToTopic("Topic1", "10", "this is message 10", new object());
                messageBroker.PublishMessageToTopic("Topic2", "11", "this is message 11", new object());
                messageBroker.PublishMessageToTopic("Topic3", "12", "this is message 12", new object());
                messageBroker.PublishMessageToTopic("Topic1", "13", "this is message 13", new object());
                messageBroker.PublishMessageToTopic("Topic3", "14", "this is message 14", new object());
                messageBroker.PublishMessageToTopic("Topic3", "15", "this is message 15", new object());

                Thread.Sleep(2000);

                messageBroker.ProcessDeadLetterQueue("Topic1");
                messageBroker.ProcessDeadLetterQueue("Topic2");
                messageBroker.ProcessDeadLetterQueue("Topic3");

            } catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
