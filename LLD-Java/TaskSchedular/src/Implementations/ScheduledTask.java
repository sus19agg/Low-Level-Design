package Implementations;

import Interfaces.ExecutionContext;
import Interfaces.Task;

import java.time.Instant;

public class ScheduledTask implements Task {

    private final ExecutionContext context;
    private Instant nextExecutionTime;
    private final boolean isRecurringTask;
    private long interval;

    public ScheduledTask(ExecutionContext context, Instant nextExecutionTime) {
        this.context = context;
        this.nextExecutionTime = nextExecutionTime;
        this.isRecurringTask = false;
    }

    public ScheduledTask(ExecutionContext context, Instant nextExecutionTime, boolean isRecurringTask, long intervalInSeconds) {
        this.context = context;
        this.nextExecutionTime = nextExecutionTime;
        this.isRecurringTask = isRecurringTask;
        this.interval = intervalInSeconds;
    }

    @Override
    public void execute() {
        this.context.execute();
    }

    @Override
    public boolean getIsRecurringTask() {
        return this.isRecurringTask;
    }

    @Override
    public Instant getNextExecutionTime() {
        return this.nextExecutionTime;
    }

    @Override
    public Task getNextScheduledTask() {
        if(this.isRecurringTask) {
            this.nextExecutionTime = Instant.now().plusSeconds(this.interval);
            return this;
        }
        return null;
    }
}
