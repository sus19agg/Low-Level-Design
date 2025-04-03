package Interfaces;

import java.time.Instant;

public interface Task {
    void execute();
    boolean getIsRecurringTask();
    Instant getNextExecutionTime();
    Task getNextScheduledTask();
}
