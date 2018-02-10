using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using DevkitApi.Model;

namespace DevkitApi.Services
{
    public class DevkitContext : DbContext
    {
       // public DbSet<Category> Categories { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Devkit> Devkits { get; set; }
        public DbSet<DevkitTools> DevkitTools { get; set; }

        public DevkitContext(DbContextOptions<DevkitContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         //   modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Tool>().ToTable("Tools");
            modelBuilder.Entity<Devkit>().ToTable("Devkit");
            modelBuilder.Entity<DevkitTools>().ToTable("DevkitTools");
        }

        public DevkitContext() { 
             
        }
        
        
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            var connString = Startup.GetConnectionString();
            System.Console.WriteLine("OnConfiguring: " + connString);
            // Select database
            //optionsBuilder.UseMySQL(connString);

        //    optionsBuilder.UseSqlite("Data Source=devkit.db");
         //   optionsBuilder.UseSqlite("Data Source=:memory:");
            

        }
    }
}

