namespace Contracts.RepositoryContracts;

using System.Linq.Expressions;

public interface IRepositoryBase<T>
{
    public Task Create(T tObj);

    public Task Update(T tObj);
    //public Task UpdateMany(Func<T, bool> predicate, T tObj);

    public Task Delete(T tObj);
    public Task<IEnumerable<T>> FindAll(bool trackChanges);
    public Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges);
    
    public Task<T> FindOne(Expression<Func<T, bool>> predicate, bool trackChanges);
}