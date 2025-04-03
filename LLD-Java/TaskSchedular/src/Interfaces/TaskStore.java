package Interfaces;

public interface TaskStore {
    void AddTask(Task task);
    Task getNextTask();
    boolean hasTask();
}
