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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}
