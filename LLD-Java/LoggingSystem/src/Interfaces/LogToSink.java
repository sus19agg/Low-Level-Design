package Interfaces;

import Enums.LogLevel;

import java.util.HashMap;

public interface LogToSink {
    void log(LogLevel level, String message, HashMap<String,Object> properties);
}
