using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Points> EarnedPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new Role());

            builder.Entity<Value>()
                .HasData(
                    new Value { Id=1, Name="EM" },
                    new Value { Id=2, Name="Chris" },
                    new Value { Id=3, Name="Mel" },
                    new Value { Id=4, Name="Regime" },
                    new Value { Id=5, Name="Berni" }
                );
        }
    }
}