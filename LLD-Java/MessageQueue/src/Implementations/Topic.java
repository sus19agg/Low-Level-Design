package Implementations;

import Interfaces.IPartition;
import Interfaces.ISubscriber;
import Interfaces.ITopic;
import Models.QueueMessage;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class Topic implements ITopic {
    private final List<IPartition> partitionList;
    private final String name;

    public Topic(String name){
        this.name = name;
        partitionList = new ArrayList<>();
        for (int i = 0; i < 3; i++) {
            partitionList.add(new Partition(name+"-"+i));
        }
    }

    @Override
    public void publishMessage(QueueMessage queueMessage) {
        Random random = new Random();
        int randomInt = random.nextInt(3);
        partitionList.get(randomInt).addNewMessage(queueMessage);
    }

    @Override
    public void addSubscriber(ISubscriber subscriber) {
        for (int i = 0; i < 3; i++) {
            partitionList.get(i).addSubscriber(subscriber.getName());
            subscriber.addPartitionToConsumeFrom(partitionList.get(i));
        }
    }

    @Override
    public void removeSubscriber(ISubscriber subscriber) {
        for (int i = 0; i < 3; i++) {
            partitionList.get(i).removeSubscriber(subscriber.getName());
            subscriber.removePartitionToConsumeFrom(partitionList.get(i));
        }
    }

    @Override
    public String getName() {
        return name;
    }
}
