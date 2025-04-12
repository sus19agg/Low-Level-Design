package Implementations;

import Interfaces.IPartition;
import Interfaces.ISubscriber;
import Models.QueueMessage;

public class SubscriberRunner implements Runnable{
    private final ISubscriber subscriber;
    private final IPartition partition;
    private volatile boolean isRunning;

    public SubscriberRunner(ISubscriber subscriber, IPartition partition){
        this.subscriber = subscriber;
        this.partition = partition;
        this.isRunning = true;
    }

    public void stop(){
        this.isRunning = false;
    }

    @Override
    public void run() {
        while(!Thread.currentThread().isInterrupted() && this.isRunning && subscriber!=null && partition!=null){
            try{
                Integer offset = partition.getOffsetForSubscriber(subscriber.getName());
                Integer lastIndex = partition.getCurrentMessageIndex();
                System.out.println("Offset: "+offset+" lastIndex: "+lastIndex);
                if(lastIndex!=-1 && offset<=lastIndex){
                    for(int i=offset;i<=lastIndex;i++){
                        if(i!=-1) {
                            QueueMessage queueMessage = partition.getMessage(i);
                            System.out.println("Partition: "+partition.getName()+" is now delivering the message");
                            subscriber.consumeMessageFromBroker(queueMessage.getId(), queueMessage.getMessage());
                            partition.updateOffsetForSubscriber(subscriber.getName(), i+1);
                        }
                    }
                }
                Thread.sleep(1000);
            } catch (InterruptedException exception){
                System.out.println("Thread interrupted "+ exception);
                isRunning = false;
            }

        }
    }
}
