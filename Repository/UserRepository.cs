namespace Repository;

using Entities;
using Contracts.RepositoryContracts;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class UserRepository : RepositoryBase<User>,  IUserRepository
{
    private readonly RepositoryContext _repositoryContext;
    public UserRepository(RepositoryContext repositoryContext): base(repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    
        public async Task<User> GetOne(Expression<Func<User, bool>> expression)
        {
            return await FindOne(expression, false);
        }

        public async void CreateUser(User user)
        {
            await Create(user);
        }

        public async Task<User> FindUser(Expression<Func<User, bool>> predicate)
        {
            return await _repoContext.Set<User>().Include((u) => u.Role).Where(predicate).FirstOrDefaultAsync();
        }
}