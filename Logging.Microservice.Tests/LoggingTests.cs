using Logging.Microservice.Interfaces;
using Logging.Microservice.Services;
using Logging.Microservice.Shared;
using Moq;
using Serilog;

namespace Logging.Microservice.Tests;

public class LoggingTests
{
    private readonly Mock<ILogger> _logger;
    private readonly ILoggingService _loggingService;
    public LoggingTests()
    {
        _logger = new Mock<ILogger>();
        _loggingService = new LoggingService(_logger.Object);
    }
    [Fact]
    public void Log_ShouldTruncateMessageWhenMessageLengthIsBiggerThan255()
    {
        var message = new string('l', 300);
        var truncatedMessage = _loggingService.TruncateMessage(message);
        Assert.Equal(255, truncatedMessage.Length);
    }

    [Fact]
    public void Log_ShouldCallInformationMethodWhenLevelIsInformation()
    {
        var message = new LogDto
        {
            Level = Level.Information,
            Message = "Test"
        };

        _loggingService.LogMessage(message);
        _logger.Verify(x => x.Information(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Log_ShouldCallWarningMethodWhenLevelIsWarning()
    {
        var message = new LogDto
        {
            Level = Level.Warning,
            Message = "Test"
        };

        _loggingService.LogMessage(message);
        _logger.Verify(x => x.Warning(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Log_ShouldCallErrorMethodWhenLevelIsError()
    {
        var message = new LogDto
        {
            Level = Level.Error,
            Message = "Test"
        };

        _loggingService.LogMessage(message);
        _logger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Log_ShouldCallFatalMethodWhenLevelIsFatal()
    {
        var message = new LogDto
        {
            Level = Level.Fatal,
            Message = "Test"
        };

        _loggingService.LogMessage(message);
        _logger.Verify(x => x.Fatal(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Log_ShouldThrowAnExceptionWhenMessageIsEmpty()
    {
        var message = new LogDto
        {
            Level = Level.Warning,
            Message = string.Empty
        };

        Assert.Throws<ArgumentNullException>(() => _loggingService.LogMessage(message));
    }

    [Fact]
    public void Log_ShouldThrowAnExceptionWhenLogIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _loggingService.LogMessage(null));
    }
}
