package Implementations;

import Interfaces.IPartition;
import Interfaces.ISubscriber;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Subscriber implements ISubscriber {

    private final String name;
    private final HashMap<IPartition, Runnable> runners;

    public Subscriber(String name){
        this.name=name;
        runners = new HashMap<>();
    }

    @Override
    public void consumeMessageFromBroker(String id, String message) {
        System.out.println("Subscriber: "+this.name+" is consuming message with id: "+id+" and message: "+message);
    }

    @Override
    public void addPartitionToConsumeFrom(IPartition partition) {
        SubscriberRunner runner = new SubscriberRunner(this,partition);
        runners.put(partition,runner);
        Thread thread = new Thread(runner);
        thread.start();
    }

    @Override
    public void removePartitionToConsumeFrom(IPartition partition) {
        SubscriberRunner runner = (SubscriberRunner) runners.get(partition);
        runner.stop();
    }

    @Override
    public String getName() {
        return name;
    }
}
