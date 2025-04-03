package Implementations;

import Interfaces.Task;
import Interfaces.TaskStore;

import java.time.Duration;
import java.time.Instant;

public class TaskSchedulerRunner implements Runnable {
    private final TaskStore taskStore;

    public TaskSchedulerRunner(TaskStore taskStore) {
        this.taskStore = taskStore;
    }

    @Override
    public void run() {
        while(!Thread.currentThread().isInterrupted()){
            try {
                if(this.taskStore.hasTask()) {
                    Task task = this.taskStore.getNextTask();
                    if(task!=null) {
                        Instant executionTime = task.getNextExecutionTime();
                        Instant now = Instant.now();
                        long diff = Duration.between(now, executionTime).toMillis();
                        if(diff>0) Thread.sleep(diff);
                        task.execute();
                        if(task.getIsRecurringTask()) {
                            taskStore.AddTask(task.getNextScheduledTask());
                        }
                    }
                }
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            } catch (Exception e) {
                // Log the exception
                e.fillInStackTrace();
            }
        }
    }
};