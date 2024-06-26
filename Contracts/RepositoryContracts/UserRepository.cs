namespace Contracts.RepositoryContracts;
using Entities;
using System.Linq.Expressions;

public interface IUserRepository
{
    public void CreateUser(User user);
    public Task<User> GetOne(Expression<Func<User, bool>> predicate);
}