import Implementations.MessageBroker;
import Implementations.Publisher;
import Implementations.Subscriber;
import Interfaces.IMessageBroker;
import Interfaces.IPublisher;
import Interfaces.ISubscriber;

import java.util.ArrayList;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        System.out.println("Hello, World!");
        
        try {
            IPublisher publisher = getPublisher();
            publisher.publishMessageToTopic("1", "This is message with ID 1","Topic-A");
            publisher.publishMessageToTopic("2", "This is message with ID 2","Topic-A");
            publisher.publishMessageToTopic("3", "This is message with ID 3","Topic-B");
            publisher.publishMessageToTopic("4", "This is message with ID 4","Topic-B");
            publisher.publishMessageToTopic("5", "This is message with ID 5","Topic-C");
            publisher.publishMessageToTopic("6", "This is message with ID 6","Topic-C");
            publisher.publishMessageToTopics("7", "This is a common message", List.of("Topic-A","Topic-B","Topic-C"));
            publisher.publishMessageToTopics("8", "This is a common message", List.of("Topic-A","Topic-B","Topic-C"));
            publisher.publishMessageToTopics("9", "This is a common message", List.of("Topic-A","Topic-B","Topic-C"));
            publisher.publishMessageToTopics("10", "This is a common message", List.of("Topic-A","Topic-B","Topic-C"));
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    private static IPublisher getPublisher() {
        IMessageBroker broker = new MessageBroker();
        broker.createTopic("Topic-A");
        broker.createTopic("Topic-B");
        broker.createTopic("Topic-C");
        ISubscriber subscriberA = new Subscriber("Subscriber-A");
        ISubscriber subscriberB = new Subscriber("Subscriber-B");
        ISubscriber subscriberC = new Subscriber("Subscriber-C");
        broker.AddSubscriberToTopic(subscriberA, "Topic-A");
        broker.AddSubscriberToTopic(subscriberB, "Topic-B");
        broker.AddSubscriberToTopic(subscriberC, "Topic-C");
        broker.AddSubscriberToTopic(subscriberA, "Topic-C");
        broker.AddSubscriberToTopic(subscriberB, "Topic-A");
        broker.AddSubscriberToTopic(subscriberC, "Topic-B");
        IPublisher publisher = new Publisher("publisher", broker);
        return publisher;
    }
}