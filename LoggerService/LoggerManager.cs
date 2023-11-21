using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager 
    {
        public static ILogger logger = LogManager.GetCurrentClassLogger();

        public void logInfo(string message)=> logger.Info(message);
        public void logWarn(string message)=> logger.Warn(message);
        public void logDebug(string message)=> logger.Debug(message);
        public void logError(string message) => logger.Error(message);

    }
}
