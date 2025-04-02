using Logging.Microservice.Shared;

namespace Logging.Microservice.Interfaces
{
    public interface ILoggingService
    {
        public string TruncateMessage(string logMessage);
        public void LogMessage(LogDto log);
    }
}
