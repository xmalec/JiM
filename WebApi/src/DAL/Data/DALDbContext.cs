using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DAL.Data
{
    public class DALDbContext : DbContext
    {
        private readonly string connectionString = "server=localhost;port=3306;database=jim;uid=root;password=root";

        public DbSet<Project> Projects { get; set; }

        public DALDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("8.0.19"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}
