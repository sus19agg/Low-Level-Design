package Interfaces;

import Enums.LogLevel;

public interface LoggerFactory {
    Logger getLogger(LogLevel logLevel);
}
