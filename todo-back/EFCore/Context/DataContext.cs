using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Context
{
    // this is one of three versions of constuctor for IdentityDbContext 
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        // Also we don't need users table because it is provided form Identity Concept
        // public DbSet<User>? Users {get; set;}
        public DbSet<TodoTask>? Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // override method

            // many-to-many relationship between users and roles => stored in UserRole table
            modelBuilder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.user)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // this will be set up from code, while implementing identity rules for validation
            // modelBuilder.Entity<User>()
            //     .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<TodoTask>().Property(c => c.Completed).HasDefaultValue(false);

            // this is default behavior from entity framework
            modelBuilder.Entity<TodoTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}