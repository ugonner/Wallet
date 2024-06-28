namespace Services;

using Contracts.ServiceContracts;
using Contracts.RepositoryContracts;
using AutoMapper;

public class ServiceManager : IServiceManager
{
    IRepositoryManager _repositoryManager;
    Lazy<IUserService> _userService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
    }

    public IUserService UserService => _userService.Value;

}