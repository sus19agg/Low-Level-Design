package Implementations;

import Enums.LogLevel;
import Interfaces.LogToSink;

import java.util.HashMap;

public class LogToDB implements LogToSink {

    private static volatile LogToSink instance;
    private static final Object lockObject = new Object();

    private LogToDB(){};

    public static LogToSink getInstance() {
        if(instance == null) {
            synchronized(lockObject){
                if (instance == null) {
                    instance = new LogToDB();
                }
            }
        }
        return instance;
    }

    @Override
    public void log(LogLevel level, String message, HashMap<String, Object> properties) {
        System.out.println("Printing to SinkType: DB");
        System.out.println("LogLevel -> "+level+" Message -> "+message+" Properties -> "+properties.toString());
    }
}
