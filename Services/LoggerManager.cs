namespace Services;

using Contracts.ServiceContracts;
public class LoggerManager : ILoggerManager
{
    public void LogInfo(string message) => Console.WriteLine(message);
    public void LogError(string message) => Console.WriteLine(message);
    public void LogWarning(string message) => Console.WriteLine(message);
}