namespace Repository;

using Contracts.RepositoryContracts;

public sealed class RepositoryManager : IRepositoryManager 
{
    Lazy<IUserRepository> userRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
    }

    public IUserRepository UserRepository => userRepository.Value;


}