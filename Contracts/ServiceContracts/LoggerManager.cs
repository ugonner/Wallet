namespace Contracts.ServiceContracts;

public interface ILoggerManager
{
    void LogInfo(string message);
    void LogError(string message);
    void LogWarning(string message);
    

}