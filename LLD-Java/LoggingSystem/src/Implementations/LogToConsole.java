package Implementations;

import Enums.LogLevel;
import Interfaces.LogToSink;
import Interfaces.LogToSinkManager;

import java.util.HashMap;

public class LogToConsole implements LogToSink {

    private static volatile LogToSink instance;
    private static final Object lockObject = new Object();

    private LogToConsole(){};

    public static LogToSink getInstance() {
        if(instance == null) {
            synchronized(lockObject){
                if (instance == null) {
                    instance = new LogToConsole();
                }
            }
        }
        return instance;
    }

    @Override
    public void log(LogLevel level, String message, HashMap<String, Object> properties) {
        System.out.println("Printing to SinkType: Console");
        System.out.println("LogLevel -> "+level+" Message -> "+message+" Properties -> "+properties.toString());
    }
}
