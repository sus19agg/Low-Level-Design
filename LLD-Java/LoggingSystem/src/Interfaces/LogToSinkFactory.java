package Interfaces;

import Enums.LogLevel;
import Enums.SinkType;

public interface LogToSinkFactory {
    LogToSink getLogToSink(SinkType sinkType);
}
