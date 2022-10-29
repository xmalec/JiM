using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DAL.Data
{
    public class DALDbContext : DbContext
    {
        public DALDbContext(DbContextOptions<DALDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProjectLog> ProjectLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }


}
