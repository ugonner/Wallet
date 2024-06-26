namespace Contracts.RepositoryContracts;

using System.Linq.Expressions;

public interface IRepositoryBase<T>
{
    public Task Create(T tObj);
    public Task CreateMany(List<T> tObj);

    public void Update(T tObj);
    public void UpdateMany(List<T> tObjs);

    public void UpdateManyByCondition(Expression<Func<T, bool>> predicate, T tObj);

    public void Delete(T tObj);
    public void DeleteMany(List<T> tObjs);
    public void DeleteManyByCondition(Func<T, bool> predicate, T tObj);
    public Task<IEnumerable<T>> FindAll(bool trackChanges);
    public Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges);
    
    public Task<T> FindOne(Expression<Func<T, bool>> predicate, bool trackChanges);
    public Task<T> FindById(int id, bool trackChanges);
}