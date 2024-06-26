namespace Contracts.RepositoryContracts;

public interface IRepositoryManager
{
    public IUserRepository UserRepository {get;}
    public IRoleRepository RoleRepository {get;}

    public Task Save();
}