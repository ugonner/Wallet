namespace Repository;
using System.Linq.Expressions;
using Contracts.RepositoryContracts;

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

    public async Task Update(T tObj)
    {
        
         _repoContext.Set<T>().Update(tObj);
        await _repoContext.SaveChangesAsync();
    }
    // public async Task UpdateMany(Func<T, bool> expression,  T tObj)
    // {
        
    //      _repoContext.Set<T>().Update(tObj).Where(expression);
    //     await _repoContext.SaveChangesAsync();
    // }

    public async Task Delete(T tObj )
    {
        
        _repoContext.Set<T>().Remove(tObj);
        await _repoContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> FindAll(bool trackChanges)
    {
        return await Task.Run( () => _repoContext.Set<T>().ToList());
    }

    public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        
        return await Task.Run(
            () =>  _repoContext.Set<T>().
        Where(expression).ToList()
        );
    }
    public async Task<T> FindOne(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        
        return await Task.Run(() => _repoContext.Set<T>()
        .SingleOrDefault(expression));
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