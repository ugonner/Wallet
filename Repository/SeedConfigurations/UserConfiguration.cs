namespace Repository.SeedConfigurations;
using Microsoft.EntityFrameworkCore;
using Entities;
public static class UserConfiguration {
    public static void ConfigureUser(this ModelBuilder builder)
    {
        builder.Entity<User>().HasData(
            new User(){
                UserId=1,
                UserName = "ugonna",
                Email = "ugonna@gmail.com",
                Password = "thanks",
                RoleId = 1
            }
        );
    }
}
