﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using File = DAL.Models.File;

namespace DAL.Data
{
    public class DALDbContext : DbContext
    {
        public DALDbContext(DbContextOptions<DALDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Entity<File>().Property(p => p.Data)
                .HasColumnType("MediumBlob");
            base.OnModelCreating(modelBuilder);
        }
    }


}