using NLog;

namespace LoggerService
{
    public class LogService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public enum LoggerLevel : int
        {
            Trace = 1,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        public static void WriteLogMessage(string message, LoggerLevel loggerLevel)
        {
            switch (loggerLevel)
            {
                case LoggerLevel.Trace:
                    Logger.Trace(message);
                    break;

                case LoggerLevel.Debug:
                    Logger.Debug(message);
                    break;

                case LoggerLevel.Info:
                    Logger.Info(message);
                    break;

                case LoggerLevel.Warn:
                    Logger.Warn(message);
                    break;

                case LoggerLevel.Error:
                    Logger.Error(message);
                    break;

                case LoggerLevel.Fatal:
                    Logger.Fatal(message);
                    break;
            }
        }
    }
}