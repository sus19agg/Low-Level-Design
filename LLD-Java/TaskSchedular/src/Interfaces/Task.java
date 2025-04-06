package Interfaces;

import Enums.TaskPriority;

import java.time.Instant;

public interface Task {
    void execute();
    Instant getNextExecutionTime();
    TaskPriority getTaskPriority();
}
