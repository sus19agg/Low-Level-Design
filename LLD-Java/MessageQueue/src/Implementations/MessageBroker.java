package Implementations;

import Interfaces.IMessageBroker;
import Interfaces.ISubscriber;
import Interfaces.ITopic;
import Models.QueueMessage;

import java.util.HashMap;

public class MessageBroker implements IMessageBroker {

    private final HashMap<String, ITopic> topics;

    public MessageBroker(){
        topics = new HashMap<>();
    }

    @Override
    public void createTopic(String topic) {
        topics.put(topic, new Topic(topic));
    }

    @Override
    public void publishMessage(String id, String message, String topic) {
        QueueMessage queueMessage = new QueueMessage(id, message, System.currentTimeMillis());
        if(topics.containsKey(topic)){
            topics.get(topic).publishMessage(queueMessage);
        }
    }

    @Override
    public void removeTopic(String topic) {
        topics.remove(topic);
    }

    @Override
    public void AddSubscriberToTopic(ISubscriber subscriber, String topic) {
        if(topics.containsKey(topic)){
            topics.get(topic).addSubscriber(subscriber);
        }
    }
}
