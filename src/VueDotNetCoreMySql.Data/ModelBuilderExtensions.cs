using Microsoft.EntityFrameworkCore;
using VueDotNetCoreMySql.Common.Entities;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Super Admin" },
            new Role { RoleId = 2, RoleName = "Admin" },
            new Role { RoleId = 3, RoleName = "User" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, UserName = "admin", Password = "admin", IsActive = true },
            new User { UserId = 2, UserName = "test", Password = "test", IsActive = true }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserRoleId = 1, UserId = 1, RoleId = 1 },
            new UserRole { UserRoleId = 2, UserId = 1, RoleId = 2 },
            new UserRole { UserRoleId = 3, UserId = 2, RoleId = 3 }
        );
    }
}