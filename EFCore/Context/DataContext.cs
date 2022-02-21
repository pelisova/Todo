using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User>? Users {get; set;}
        public DbSet<TodoTask>? Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder); // override method

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<TodoTask>().Property(c => c.Completed).HasDefaultValue(false);

            // modelBuilder.Entity<TodoTask>()
            //     .HasOne(t => t.User)
            //     .WithMany(u => u.Tasks)
            //     .OnDelete(DeleteBehavior.Cascade);  
        }

    }
}