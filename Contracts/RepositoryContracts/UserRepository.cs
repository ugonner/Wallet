namespace Contracts.RepositoryContracts;
using Entities;
using System.Linq.Expressions;

public interface IUserRepository : IRepositoryBase<User>
{
    public void CreateUser(User user);

    public Task<User> FindUser(Expression<Func<User, bool>> predicate);
    public Task<User> GetOne(Expression<Func<User, bool>> predicate);
}