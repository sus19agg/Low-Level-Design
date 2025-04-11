package Implementations;

import Interfaces.IPartition;
import Models.QueueMessage;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Partition implements IPartition {

    private final List<QueueMessage> messageList;
    private final HashMap<String,Integer> offsets;
    private final String name;

    public Partition(String name){
        messageList = new ArrayList<>();
        offsets = new HashMap<>();
        this.name=name;
    }

    @Override
    public Integer getOffsetForSubscriber(String subscriber) {
        if(offsets.containsKey(subscriber)) {
            return offsets.get(subscriber);
        }
        return null;
    }

    @Override
    public void addSubscriber(String subscriber) {
        offsets.put(subscriber, messageList.size()-1);
    }

    @Override
    public void removeSubscriber(String subscriber) {
        offsets.remove(subscriber);
    }

    @Override
    public void addNewMessage(QueueMessage queueMessage) {
        messageList.add(queueMessage);
    }

    @Override
    public Integer getCurrentMessageIndex() {
        return messageList.size()-1;
    }

    @Override
    public void updateOffsetForSubscriber(String subscriber, Integer offset) {
        offsets.put(subscriber,offset);
    }

    @Override
    public QueueMessage getMessage(Integer index) {
        if(index<messageList.size()) return messageList.get(index);
        return null;
    }

    @Override
    public String getName() {
        return name;
    }
}
