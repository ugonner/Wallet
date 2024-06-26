namespace Repository;

using Entities;
using Contracts.RepositoryContracts;

using System.Linq.Expressions;

public class UserRepository : RepositoryBase<User>,  IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext): base(repositoryContext)
    {}

    
        public async Task<User> GetOne(Expression<Func<User, bool>> expression)
        {
            return await FindOne(expression, false);
        }

        public async void CreateUser(User user)
        {
            await Create(user);
        }
}