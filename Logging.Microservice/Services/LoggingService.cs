using Logging.Microservice.Interfaces;
using Logging.Microservice.Shared;
using Serilog;

namespace Logging.Microservice.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly Serilog.ILogger _logger;

        public LoggingService(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public void LogMessage(LogDto log)
        {
            if (log == null || string.IsNullOrEmpty(log.Message))
                throw new ArgumentNullException("Log message is required");

            var message = TruncateMessage(log.Message);


            switch (log.Level)
            {
                case Level.Fatal:
                    _logger.Fatal(message);
                    break;
                case Level.Error:
                    _logger.Error(message);
                    break;
                case Level.Warning:
                    _logger.Warning(message);
                    break;
                case Level.Information:
                    _logger.Information(message);
                    break;
                default:
                    _logger.Information(message);
                    break;
            }
        }

        public string TruncateMessage(string logMessage)
        {
            return logMessage.Length > 255 ? logMessage[..252] + "..." : logMessage;
        }
    }
}
