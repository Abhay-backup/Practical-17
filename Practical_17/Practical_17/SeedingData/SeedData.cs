using Microsoft.EntityFrameworkCore;
using Practical_17.Models;

namespace Practical_17.SeedingData
{
    public static class SeedData
    {

        public static void SeedRole(this ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                    new Role()
                    {
                        Id = 1,
                        RoleName = "Admin",
                    },
                    new Role()
                    {
                        Id = 2,
                        RoleName = "User",
                    });
        }

        public static void SeedAdmin(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                    new User()
                    {
                        Id = 1,
                        MobileNumber = "1234567890",
                        FirstName = "Admin",
                        LastName = "admin",
                        Email = "admin@gmail.com",
                        Password = "Admin",
                    });
        }

        public static void SeedUserRole(this ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasData(
                new UserRole()
                {
                    RoleId = 1,

                    UserId = 1,

                });
        }

    }
}
