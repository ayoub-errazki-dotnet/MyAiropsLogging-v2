using Logging.Microservice.Interfaces;
using Logging.Microservice.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
builder.Services.AddScoped<ILoggingService, LoggingService>();

var startingMessage = "Application [LoggingMicroservice] started";
Log.Logger.Information(startingMessage);

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => startingMessage);
app.Run();
