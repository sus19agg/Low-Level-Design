package Implementations;

import Enums.LogLevel;
import Interfaces.LogToSinkManager;
import Interfaces.Logger;

import java.util.HashMap;

public class ErrorLogger implements Logger {
    private Logger nextLogger;

    @Override
    public void setNextLogger(Logger logger) {
        this.nextLogger = logger;
    }

    @Override
    public void log(LogLevel level, String message, HashMap<String, Object> properties, LogToSinkManager sinkManager) {
        if(level == LogLevel.ERROR) {
            sinkManager.logToSinks(level,message,properties);
        } else if(nextLogger!=null){
            nextLogger.log(level,message,properties, sinkManager);
        }
    }
}
