package Interfaces;

import java.util.List;

public interface IPublisher {
    void publishMessageToTopic(String id, String message, String topic);
    void publishMessageToTopics(String id, String message, List<String> topics);
    String getName();
}
