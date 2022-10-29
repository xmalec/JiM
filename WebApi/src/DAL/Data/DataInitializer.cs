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

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    ProjectName = "Labclub",
                    Description = "Description",
                    LongDescription = "Long Description"
                }
            );

            #endregion
        }
    }
}
