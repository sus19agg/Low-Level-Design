package Implementations;

import Interfaces.Task;
import Interfaces.TaskStore;

import java.util.Comparator;
import java.util.PriorityQueue;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

public class TaskStoreImpl implements TaskStore {

    private final PriorityQueue<Task> taskQueue;
    private static final Object lockObject = new Object();
    private static TaskStore store;

    private TaskStoreImpl() {
        Comparator<Task> taskComparator = new Comparator<Task>() {
            @Override
            public int compare(Task t1, Task t2) {
                int x = t1.getNextExecutionTime().compareTo(t2.getNextExecutionTime());
                if(x==0){
                    return t1.getTaskPriority().compareTo(t2.getTaskPriority());
                }
                return x;
            }
        };
        taskQueue = new PriorityQueue<Task>(taskComparator);
    }

    public static TaskStore getStore() {
        if(store == null) {
            synchronized (lockObject) {
                store = new TaskStoreImpl();
            }
        }
        return store;
    }

    @Override
    public void AddTask(Task task) {
        synchronized (lockObject) {
            this.taskQueue.add(task);
        }
    }

    @Override
    public Task getNextTask() {
        synchronized (lockObject) {
            return this.taskQueue.poll();
        }
    }

    @Override
    public boolean hasTask() {
        synchronized (lockObject) {
            return !this.taskQueue.isEmpty();
        }
    }
}
