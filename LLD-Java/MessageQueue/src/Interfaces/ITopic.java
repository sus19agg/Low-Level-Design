package Interfaces;

import Models.QueueMessage;

public interface ITopic {
    void publishMessage(QueueMessage queueMessage);
    void addSubscriber(ISubscriber subscriber);
    void removeSubscriber(ISubscriber subscriber);
    String getName();
}
