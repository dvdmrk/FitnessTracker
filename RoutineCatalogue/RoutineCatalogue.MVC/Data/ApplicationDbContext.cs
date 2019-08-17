using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoutineCatalogue.Models.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RoutineCatalogue.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
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
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var change in base.ChangeTracker.Entries().ToList())
                if (change.Entity is AuditableEntity)
                {
                    var identity = (_httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier);
                    if (change.State == EntityState.Added)
                    {
                        (change.Entity as AuditableEntity).CreateDate = DateTime.Now;
                        (change.Entity as AuditableEntity).CreateBy = Set<User>().Find(Guid.Parse(identity.Value));
                    }
                    else if (change.State == EntityState.Modified)
                    {
                        (change.Entity as AuditableEntity).UpdateDate = DateTime.Now;
                        (change.Entity as AuditableEntity).UpdateBy = Set<User>().Find(Guid.Parse(identity.Value));
                    }
                }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}