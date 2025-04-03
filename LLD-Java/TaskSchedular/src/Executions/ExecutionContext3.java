package Executions;

import Interfaces.ExecutionContext;

import java.time.Instant;

public class ExecutionContext3 implements ExecutionContext {
    @Override
    public void execute() {
        System.out.println("I am now executing the ExecutionContext 3 - "+ Instant.now().toEpochMilli());
    }
}
