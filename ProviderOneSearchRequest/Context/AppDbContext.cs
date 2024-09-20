using Microsoft.EntityFrameworkCore;
using ProviderOne.Context.Entities;

namespace ProviderOne.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<OneRoute> OneRoutes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var route1 = new OneRoute
            {
                Id = 1,
                From = "Moscow",
                To = "SPB",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddHours(4),
                Price = 15000,
                TimeLimit = DateTime.Now.AddHours(5)
            };

            modelBuilder.Entity<OneRoute>().HasData(route1);

            var route2 = new OneRoute
            {
                Id = 2,
                From = "Vladivostok",
                To = "Voronez",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddHours(12),
                Price = 45000,
                TimeLimit = DateTime.Now.AddHours(13)
            };

            modelBuilder.Entity<OneRoute>().HasData(route2);
        }
    }
}
