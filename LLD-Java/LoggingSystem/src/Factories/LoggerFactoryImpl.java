package Factories;

import Enums.LogLevel;
import Implementations.DebugLogger;
import Implementations.ErrorLogger;
import Implementations.InfoLogger;
import Interfaces.LogToSinkManager;
import Interfaces.Logger;
import Interfaces.LoggerFactory;

public class LoggerFactoryImpl implements LoggerFactory {
    @Override
    public Logger getLogger(LogLevel logLevel) {
        switch (logLevel) {
            case INFO -> {
                return new InfoLogger();
            }
            case DEBUG -> {
                return new DebugLogger();
            }
            case ERROR -> {
                return new ErrorLogger();
            }
        }
        return null;
    }
}
