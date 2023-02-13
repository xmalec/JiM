using BCrypt.Net;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using File = DAL.Models.File;

namespace DAL.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
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
            );
            byte[] imageData = null;

            using (var wc = new System.Net.WebClient())
                imageData = wc.DownloadData("https://cdn.alza.cz/ImgW.ashx?fd=f16&cd=SPTstorm023");
            modelBuilder.Entity<File>().HasData(
                new File
                {
                    Id = 1,
                    Size = imageData.Length,
                    Extension = "jpif",
                    FileType = "image/jpeg",
                    Path = "MediaLibrary/Subfolder/ImgW.jfif",
                    Name = "Sample image",
                    Width = 800,
                    Height = 800
                }
            );
        }
    }
}
