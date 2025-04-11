package Interfaces;

import Models.QueueMessage;

public interface IPartition {
    Integer getOffsetForSubscriber(String subscriber);
    void addSubscriber(String subscriber);
    void removeSubscriber(String subscriber);
    void addNewMessage(QueueMessage queueMessage);
    Integer getCurrentMessageIndex();
    void updateOffsetForSubscriber(String subscriber, Integer offset);
    QueueMessage getMessage(Integer index);
    String getName();
}
