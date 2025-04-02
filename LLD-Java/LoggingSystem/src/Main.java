import Enums.LogLevel;
import Enums.SinkType;
import Factories.LoggerFactoryImpl;
import Implementations.LogManagerImpl;
import Implementations.LogToSinkManagerImpl;
import Interfaces.LogManager;
import Interfaces.LogToSinkManager;
import Interfaces.Logger;
import Interfaces.LoggerFactory;

import java.util.HashMap;

public class Main {
    public static void main(String[] args) {
        //prepare sink manager
        LogToSinkManager sinkManager = LogToSinkManagerImpl.getInstance();
        sinkManager.addSinkToLog(LogLevel.DEBUG, SinkType.CONSOLE);
        sinkManager.addSinkToLog(LogLevel.DEBUG, SinkType.FILE);
        sinkManager.addSinkToLog(LogLevel.INFO, SinkType.CONSOLE);
        sinkManager.addSinkToLog(LogLevel.INFO, SinkType.FILE);
        sinkManager.addSinkToLog(LogLevel.ERROR, SinkType.CONSOLE);
        sinkManager.addSinkToLog(LogLevel.ERROR, SinkType.DB);

        //prepare loggers
        LogManager logManager = LogManagerImpl.getInstance(sinkManager);
        LoggerFactory loggerFactory = new LoggerFactoryImpl();
        Logger infoLogger = loggerFactory.getLogger(LogLevel.INFO);
        Logger debugLogger = loggerFactory.getLogger(LogLevel.DEBUG);
        Logger errorLogger = loggerFactory.getLogger(LogLevel.ERROR);
        infoLogger.setNextLogger(errorLogger);
        debugLogger.setNextLogger(infoLogger);
        logManager.setStartingLevelLogger(debugLogger);

        logManager.log(LogLevel.DEBUG, "This is a sample debug message", new HashMap<>());
        logManager.log(LogLevel.INFO, "This is a sample info message", new HashMap<>());
        logManager.log(LogLevel.ERROR, "This is a sample error message", new HashMap<>());
    }
}