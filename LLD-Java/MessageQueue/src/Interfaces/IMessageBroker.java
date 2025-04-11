package Interfaces;

public interface IMessageBroker {
    void createTopic(String topic);
    void publishMessage(String id, String message, String topic);
    void removeTopic(String topic);
    void AddSubscriberToTopic(ISubscriber subscriber, String topic);
}
