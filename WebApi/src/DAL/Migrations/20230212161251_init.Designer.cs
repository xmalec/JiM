﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DALDbContext))]
    [Migration("20230212161251_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DAL.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Files");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2023, 2, 12, 17, 12, 51, 95, DateTimeKind.Local).AddTicks(2584),
                            Extension = "jpif",
                            FileType = "image/jpeg",
                            Height = 800,
                            Name = "Sample image",
                            Path = "MediaLibrary/Subfolder/ImgW.jfif",
                            Size = 71033,
                            Width = 800
                        });
                });

            modelBuilder.Entity("DAL.Models.ScheduleTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastRun")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ScheduleTasks");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin1@domain.com",
                            FirstName = "Ladislav",
                            IsAdmin = true,
                            LastName = "Košíček",
                            PasswordHash = "$2a$11$NvaUJyVAXaKidcMoa.YHSegmIG6mwsVz0b.dO8xux2r1WzR3HVyS6"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user1@domain.com",
                            FirstName = "Monika",
                            IsAdmin = false,
                            LastName = "Nejedlá",
                            PasswordHash = "$2a$11$aUgYfKi/.HCG8E.EIgrc6e53fZh9nIfOLwsD3EWCLpmf5UKweMeJC"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}