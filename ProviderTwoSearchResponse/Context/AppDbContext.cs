using Microsoft.EntityFrameworkCore;
using ProviderTwo.Context.Entities;

namespace ProviderTwo.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<TwoRoute> TwoRoutes { get; set; }

        public DbSet<TwoPont> TwoPonts { get; set; }

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

            var route1 = new TwoRoute
            {
                Id = 1,
                Price = 15000,
                TimeLimit = DateTime.Now.AddHours(5)
            };

            modelBuilder.Entity<TwoRoute>().HasData(route1);

            var point1Route1 = new TwoPont()
            {
                Id = 1,
                TwoRouteId = route1.Id,
                Date = DateTime.Now,
                Point = "Moscow",
                PointType = TwoPointType.Departure,
            };


            modelBuilder.Entity<TwoPont>().HasData(point1Route1);

            var point2Route1 = new TwoPont()
            {
                Id = 2,
                TwoRouteId = route1.Id,
                Date = DateTime.Now.AddHours(3),
                Point = "Sochi",
                PointType = TwoPointType.Arrival,
            };

            modelBuilder.Entity<TwoPont>().HasData(point2Route1);

            var route2 = new TwoRoute
            {
                Id = 2,
                Price = 45000,
                TimeLimit = DateTime.Now.AddHours(13)
            };

            modelBuilder.Entity<TwoRoute>().HasData(route2);

            var point1Route2 = new TwoPont()
            {
                Id = 3,
                TwoRouteId = route2.Id,
                Date = DateTime.Now,
                Point = "SPB",
                PointType = TwoPointType.Departure,
            };


            modelBuilder.Entity<TwoPont>().HasData(point1Route2);

            var point2Route2 = new TwoPont()
            {
                Id = 4,
                TwoRouteId = route2.Id,
                Date = DateTime.Now.AddHours(7),
                Point = "Novosiborck",
                PointType = TwoPointType.Arrival,
            };

            modelBuilder.Entity<TwoPont>().HasData(point2Route2);
        }
    }
}
