import Enums.TaskPriority;
import Executions.ExecutionContext1;
import Executions.ExecutionContext2;
import Executions.ExecutionContext3;
import Implementations.RecurringScheduledTask;
import Implementations.ScheduledTask;
import Implementations.TaskSchedulerImpl;
import Interfaces.ExecutionContext;
import Interfaces.TaskScheduler;

import java.time.Instant;

public class Main {
    public static void main(String[] args) {
        int parallelExecution = 3;
        TaskScheduler taskScheduler = TaskSchedulerImpl.getInstance(parallelExecution);

        ExecutionContext executionContext1 = new ExecutionContext1();
        ExecutionContext executionContext2 = new ExecutionContext2();
        ExecutionContext executionContext3 = new ExecutionContext3();

        taskScheduler.scheduleTask(new RecurringScheduledTask(executionContext1, Instant.now(), 2, TaskPriority.LOW));
        taskScheduler.scheduleTask(new ScheduledTask(executionContext2, Instant.now(), TaskPriority.HIGH));
        taskScheduler.scheduleTask(new RecurringScheduledTask(executionContext3, Instant.now(), 3, TaskPriority.MEDIUM));
        taskScheduler.scheduleTask(new ScheduledTask(executionContext2, Instant.now(), TaskPriority.CRITICAL));

        taskScheduler.start();
        try {
            Thread.sleep(10000);
            taskScheduler.stop();
        } catch (Exception e){
            System.out.println(e);
        }
    }
}
