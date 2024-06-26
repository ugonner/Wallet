namespace Contracts.ServiceContracts;

public interface IServiceManager
{
    public IUserService UserService {get; }
}