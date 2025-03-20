using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"INFO: {DateTime.UtcNow} - {message}");
    }

    public void LogError(string message)
    {
        Console.WriteLine($"ERROR: {DateTime.UtcNow} - {message}");
    }
}