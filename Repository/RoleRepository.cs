using Contracts.RepositoryContracts;
using Entities;

namespace Repository;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(RepositoryContext repositoryContext): base(repositoryContext)
    {}


        
}
