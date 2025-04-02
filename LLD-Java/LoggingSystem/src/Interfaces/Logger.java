package Interfaces;

import Enums.LogLevel;

import java.util.HashMap;

public interface Logger {
    void setNextLogger(Logger logger);
    void log(LogLevel level, String message, HashMap<String,Object> properties, LogToSinkManager sinkManager);
}
