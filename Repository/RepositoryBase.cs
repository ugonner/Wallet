namespace Repository;
using System.Linq.Expressions;
using Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext _repoContext;
    public RepositoryBase(RepositoryContext repositoryContet)
    {
        _repoContext = repositoryContet;
    }

    
    public async Task Create(T tObj)
    {
        await _repoContext.Set<T>().AddAsync(tObj);
    }
    public async Task CreateMany(List<T> tObjs)
    {
        await _repoContext.Set<T>().AddRangeAsync(tObjs);
    }

    public void Update(T tObj)
    {
        
         _repoContext.Set<T>().Update(tObj);
    }


    public void UpdateMany(List<T> tObjs )
    {
        _repoContext.Set<T>().UpdateRange(tObjs);
    }
    public void UpdateManyByCondition(Expression<Func<T, bool>> expression,  T tObj)
    {
        
         var entities = _repoContext.Set<T>().Where(expression).ToList<T>();
         entities.Select((T e) => tObj);
         
         _repoContext.Set<T>().UpdateRange(entities);
    }

    public void Delete(T tObj )
    {
        _repoContext.Set<T>().Remove(tObj);
    }
    public void DeleteMany(List<T> tObjs )
    {
        _repoContext.Set<T>().RemoveRange(tObjs);
    }
    
    public void DeleteManyByCondition(Func<T, bool> expression,  T tObj)
    {
        
         var entities = _repoContext.Set<T>().Where(expression).ToList<T>();
         entities.Select((T e) => tObj);
         
         _repoContext.Set<T>().RemoveRange(entities);
    }

    public async Task<IEnumerable<T>> FindAll(bool trackChanges)
    {
        return await Task.Run( () => _repoContext.Set<T>().ToList());
    }

    public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        
        return await  _repoContext.Set<T>().
        Where(expression).ToListAsync<T>();
        
    }
    public async Task<T> FindOne(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        
         return  await _repoContext.Set<T>()
        .FirstOrDefaultAsync<T>(expression);
    }

    public async Task<T> FindById(int id, bool trackChanges)
    {
        return await _repoContext.Set<T>().FindAsync(id);
    } 

    // public async Task<List<YourEntity>> SearchEntitiesAsync(string pattern)
    // {
    //     string likePattern = $"%{pattern}%";
    //     return await _context.YourEntities
    //         .Where(e => EF.Functions.Like(e.Field1, likePattern) ||
    //                     EF.Functions.Like(e.Field2, likePattern) ||
    //                     EF.Functions.Like(e.Field3, likePattern))
    //         .ToListAsync();
    // }

}