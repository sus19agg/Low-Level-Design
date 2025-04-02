package Interfaces;

import Enums.LogLevel;

import java.util.HashMap;

public interface LogManager {
    void setStartingLevelLogger(Logger logger);
    void log(LogLevel level, String message, HashMap<String,Object> properties);
}
