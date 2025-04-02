package Implementations;

import Enums.LogLevel;
import Enums.SinkType;
import Factories.LogToSinkFactoryImpl;
import Interfaces.LogToSink;
import Interfaces.LogToSinkFactory;
import Interfaces.LogToSinkManager;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class LogToSinkManagerImpl implements LogToSinkManager {

    private HashMap<LogLevel, List<LogToSink>> logLevelToSinks;
    private static volatile LogToSinkManager logToSinkManagerInstance;
    private static final Object lockObject = new Object();

    private LogToSinkManagerImpl(){
        logLevelToSinks = new HashMap<LogLevel, List<LogToSink>>();
    };

    public static LogToSinkManager getInstance() {
        if(logToSinkManagerInstance == null) {
            synchronized(lockObject){
                if (logToSinkManagerInstance == null) {
                    logToSinkManagerInstance = new LogToSinkManagerImpl();
                }
            }
        }
        return logToSinkManagerInstance;
    }

    @Override
    public void addSinkToLog(LogLevel level, SinkType sinkType) {
        LogToSinkFactory sinkFactory = new LogToSinkFactoryImpl();
        LogToSink sink = sinkFactory.getLogToSink(sinkType);
        if(logLevelToSinks.containsKey(level)){
            logLevelToSinks.get(level).add(sink);
        } else {
            logLevelToSinks.put(level, new ArrayList<LogToSink>());
            logLevelToSinks.get(level).add(sink);
        }
    }

    @Override
    public void removeSinkFromLog(LogLevel level, LogToSink sink) {
        if(logLevelToSinks.containsKey(level)){
            List<LogToSink> sinks = logLevelToSinks.get(level);
            LogToSink sinkToRemove = null;
            for(int i=0; i< sinks.size(); i++) {
                if(sinks.get(i).getClass() == sink.getClass()) {
                    sinkToRemove = sinks.get(i);
                    break;
                }
            }
            if(sinkToRemove!=null) {
                sinks.remove(sinkToRemove);
            }
        }
    }

    @Override
    public void logToSinks(LogLevel level, String message, HashMap<String, Object> properties) {
        if(logLevelToSinks.containsKey(level)){
            for(LogToSink sink: logLevelToSinks.get(level)){
                sink.log(level,message, properties);
            }
        }
    }
}
