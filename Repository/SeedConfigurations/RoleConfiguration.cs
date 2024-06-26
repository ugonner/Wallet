namespace Repository.SeedConfigurations;
using Microsoft.EntityFrameworkCore;
using Entities;
public static class RoleConfiguration {
    public static void ConfigureRoles(this ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
            new Role(){
                Id=1,
                RoleName = "Admin"
            },
            new Role(){
                Id=2,
                RoleName = "Professional"
            },
            new Role(){
                Id=3,
                RoleName = "User"
            }
        );
    }
}
