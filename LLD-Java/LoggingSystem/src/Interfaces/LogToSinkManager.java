package Interfaces;

import Enums.LogLevel;
import Enums.SinkType;

import java.util.HashMap;

public interface LogToSinkManager {
    void addSinkToLog(LogLevel level, SinkType sinkType);
    void removeSinkFromLog(LogLevel level, LogToSink sink);
    void logToSinks(LogLevel level, String message, HashMap<String,Object> properties);
}
