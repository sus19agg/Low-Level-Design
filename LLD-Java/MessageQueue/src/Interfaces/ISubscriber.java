package Interfaces;

import java.util.List;

public interface ISubscriber {
    void consumeMessageFromBroker(String id, String message);
    void addPartitionToConsumeFrom(IPartition partition);
    void removePartitionToConsumeFrom(IPartition partition);
    String getName();
}
