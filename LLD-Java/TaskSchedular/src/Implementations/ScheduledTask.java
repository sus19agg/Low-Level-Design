package Implementations;

import Enums.TaskPriority;
import Interfaces.ExecutionContext;
import Interfaces.RecurringTask;
import Interfaces.Task;

import java.time.Instant;

public class ScheduledTask implements Task {

    private final ExecutionContext context;
    private final Instant nextExecutionTime;
    private final TaskPriority priority;

    public ScheduledTask(ExecutionContext context, Instant nextExecutionTime, TaskPriority priority) {
        this.context = context;
        this.nextExecutionTime = nextExecutionTime;
        this.priority = priority;
    }

    @Override
    public void execute() {
        this.context.execute();
    }

    @Override
    public TaskPriority getTaskPriority() {
        return priority;
    }

    @Override
    public Instant getNextExecutionTime() {
        return this.nextExecutionTime;
    }
}
