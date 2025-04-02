package Factories;

import Enums.SinkType;
import Implementations.LogToConsole;
import Implementations.LogToDB;
import Implementations.LogToFile;
import Interfaces.LogToSink;
import Interfaces.LogToSinkFactory;

public class LogToSinkFactoryImpl implements LogToSinkFactory {
    @Override
    public LogToSink getLogToSink(SinkType sinkType) {
        switch (sinkType) {
            case CONSOLE -> {
                return LogToConsole.getInstance();
            }
            case FILE -> {
                return LogToFile.getInstance();
            }
            case DB -> {
                return LogToDB.getInstance();
            }
        }
        return null;
    }
}
