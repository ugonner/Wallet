namespace Repository;

using Contracts.RepositoryContracts;

public sealed class RepositoryManager : IRepositoryManager 
{
    Lazy<IUserRepository> userRepository;
    Lazy<IRoleRepository> _roleRepository;
    RepositoryContext _repositoryContext;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(repositoryContext));
    }

    public IUserRepository UserRepository => userRepository.Value;
    public IRoleRepository RoleRepository => _roleRepository.Value;
    public async Task Save() => await _repositoryContext.SaveChangesAsync();
    

    

}