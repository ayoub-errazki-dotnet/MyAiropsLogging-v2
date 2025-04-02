using System.ComponentModel.DataAnnotations;

namespace Logging.Microservice.Shared
{
    public enum Level { Fatal, Error, Warning, Information };
    public class LogDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(255)]
        public required string Message { get; set; }

        public required Level Level { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
