namespace BFFApi.ContextFactory;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Repository;
public class RepositoryContextFactory: IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();

        var builder = new DbContextOptionsBuilder()
        .UseMySQL(config.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("BFFApi"));
        
        return new RepositoryContext(builder.Options);
    }
}