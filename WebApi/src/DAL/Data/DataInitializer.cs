using BCrypt.Net;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Admins

            Guid user1Id = Guid.Parse("685241db-073e-4323-a7cc-eda680b2b0bb");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Ladislav",
                    LastName = "Košíček",
                    Email = "admin1@domain.com",
                    IsAdmin = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin")
                },
                new User
                {
                    Id = 2,
                    FirstName = "Monika",
                    LastName = "Nejedlá",
                    Email = "user1@domain.com",
                    IsAdmin = false,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user")
                }
            ); ;

            #endregion
        }
    }
}
