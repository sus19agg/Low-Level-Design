package Interfaces;

public interface TaskScheduler {
    void scheduleTask(Task task);
    void start();
    void stop();
}
