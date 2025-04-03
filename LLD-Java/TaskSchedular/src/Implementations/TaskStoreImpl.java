package Implementations;

import Interfaces.Task;
import Interfaces.TaskStore;

import java.util.Comparator;
import java.util.PriorityQueue;

public class TaskStoreImpl implements TaskStore {

    private final PriorityQueue<Task> taskQueue;
    private static final Object lockObject = new Object();

    public TaskStoreImpl() {
        Comparator<Task> taskComparator = new Comparator<Task>() {
            @Override
            public int compare(Task t1, Task t2) {
                return t1.getNextExecutionTime().compareTo(t2.getNextExecutionTime());
            }
        };
        taskQueue = new PriorityQueue<Task>(taskComparator);
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
