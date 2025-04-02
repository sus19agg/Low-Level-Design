package Implementations;

import Enums.LogLevel;
import Interfaces.LogManager;
import Interfaces.LogToSinkManager;
import Interfaces.Logger;

import java.util.HashMap;

public class LogManagerImpl implements LogManager {
    private Logger startingLogger;
    private final LogToSinkManager sinkManager;
    private static volatile LogManager instance;
    private static final Object lockObject = new Object();

    private LogManagerImpl(LogToSinkManager sinkManager) {
        this.sinkManager = sinkManager;
    }

    public static LogManager getInstance (LogToSinkManager sinkManager) {
        if(instance==null){
            synchronized (lockObject) {
                if(instance==null) {
                    instance = new LogManagerImpl(sinkManager);
                }
            }
        }
        return instance;
    }

    @Override
    public void setStartingLevelLogger(Logger logger) {
        this.startingLogger = logger;
    }

    @Override
    public void log(LogLevel level, String message, HashMap<String, Object> properties) {
        if(startingLogger!=null) {
            startingLogger.log(level,message,properties,this.sinkManager);
        }
    }
}
