package Implementations;

import Interfaces.IMessageBroker;
import Interfaces.IPublisher;

import java.util.List;

public class Publisher implements IPublisher {
    private final String name;
    private final IMessageBroker broker;

    public Publisher(String name, IMessageBroker broker){
        this.name = name;
        this.broker = broker;
    }

    @Override
    public void publishMessageToTopic(String id, String message, String topic) {
        this.broker.publishMessage(id,message,topic);
    }

    @Override
    public void publishMessageToTopics(String id, String message, List<String> topics) {
        for(String topic: topics){
            this.broker.publishMessage(id,message,topic);
        }
    }

    @Override
    public String getName() {
        return name;
    }
}
