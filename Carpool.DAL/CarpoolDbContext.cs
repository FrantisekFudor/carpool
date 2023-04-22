using Microsoft.EntityFrameworkCore;
using Carpool.DAL.Entities;
using Carpool.DAL.Seeds;

namespace Carpool.DAL
{
    public class CarpoolDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        public CarpoolDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
            : base(contextOptions)
        {
            _seedDemoData = seedDemoData;
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<PassengerEntity> Passengers => Set<PassengerEntity>();
        public DbSet<RideEntity> Rides => Set<RideEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasMany(c => c.Cars)
                .WithOne(o => o.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany(r => r.RidesAsDriver)
                .WithOne(d => d.Driver)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PassengerEntity>()
                .HasOne<UserEntity>(u => u.User)
                .WithMany(r => r.RidesAsPassenger)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<PassengerEntity>()
                .HasOne<RideEntity>(r => r.Ride)
                .WithMany(p => p.Passengers)
                .HasForeignKey(r => r.RideId);

            modelBuilder.Entity<RideEntity>()
                .HasOne(d => d.Driver)
                .WithMany(r => r.RidesAsDriver)
                .HasForeignKey(d => d.DriverId);

            if (_seedDemoData)
            {
                UserSeeds.Seed(modelBuilder);
                CarSeeds.Seed(modelBuilder);
                RideSeeds.Seed(modelBuilder);
                PassengerSeeds.Seed(modelBuilder);
            }
        }
    }
}
