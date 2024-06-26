namespace Repository;
using Microsoft.EntityFrameworkCore;
using Entities;
using Repository.SeedConfigurations;
public class RepositoryContext: DbContext
{
    public RepositoryContext(DbContextOptions option): base(option) 
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureUser();
    }

    DbSet<User> Users {get; set;}
}
