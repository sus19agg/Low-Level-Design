package Implementations;

import Enums.TaskPriority;
import Interfaces.ExecutionContext;
import Interfaces.RecurringTask;
import Interfaces.Task;

import java.time.Instant;

public class RecurringScheduledTask implements Task, RecurringTask {

    private final ExecutionContext context;
    private Instant nextExecutionTime;
    private final long interval;
    private final TaskPriority priority;

    public RecurringScheduledTask(ExecutionContext context, Instant nextExecutionTime, long intervalInSeconds, TaskPriority priority) {
        this.context = context;
        this.nextExecutionTime = nextExecutionTime;
        this.interval = intervalInSeconds;
        this.priority = priority;
    }

    @Override
    public void execute() {
        this.context.execute();
    }

    @Override
    public Instant getNextExecutionTime() {
        return this.nextExecutionTime;
    }

    @Override
    public TaskPriority getTaskPriority() {
        return priority;
    }

    @Override
    public Task getNextScheduledTask() {
        this.nextExecutionTime = Instant.now().plusSeconds(this.interval);
        return this;
    }
}
