using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.Entities;
using System;
namespace RoutineCatalogue.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Generate Ids on Add
            builder.Entity<Routine>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<Exercise>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            builder.Entity<Set>(b => { b.Property(u => u.Id).ValueGeneratedOnAdd(); });
            //One to Many Relationships
            builder.Entity<Set>().HasOne(c => c.Routine).WithMany(e => e.Sets);
        }
        public DbSet<Routine> Routine { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<User> User { get; set; }
    }
}