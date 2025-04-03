package Implementations;

import Interfaces.Task;
import Interfaces.TaskScheduler;
import Interfaces.TaskStore;

import java.util.ArrayList;
import java.util.List;

public class TaskSchedulerImpl implements TaskScheduler {

    private final int noOfThreads;
    private final TaskStore taskStore;
    private static final Object lockObject = new Object();
    private static TaskScheduler instance;
    private final List<Thread> threads;

    private TaskSchedulerImpl(int noOfThreads) {
        this.noOfThreads = noOfThreads;
        taskStore = new TaskStoreImpl();
        threads = new ArrayList<>();
    }

    public static TaskScheduler getInstance(int noOfThreads) {
        if(instance == null) {
            synchronized (lockObject) {
                instance = new TaskSchedulerImpl(noOfThreads);
            }
        }
        return instance;
    }

    private void prepareThreads() {
        for(int i=0;i<this.noOfThreads;i++){
            Thread thread = new Thread(new TaskSchedulerRunner(this.taskStore));
            this.threads.add(thread);
        }
    }

    @Override
    public void scheduleTask(Task task) {
        this.taskStore.AddTask(task);
    }

    @Override
    public void start() {
        this.prepareThreads();
        for(int i=0;i<this.noOfThreads;i++){
            this.threads.get(i).start();
        }
    }

    @Override
    public void stop() {
        for(int i=0;i<this.noOfThreads;i++){
            this.threads.get(i).interrupt();
        }
    }
}
