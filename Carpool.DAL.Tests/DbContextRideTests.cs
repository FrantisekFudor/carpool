using System;
using System.Linq;
using System.Threading.Tasks;
using Carpool.DAL.Seeds;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using System.Globalization;

namespace Carpool.DAL.Tests
{
    public class DbContextRideTests : DbContextTestsBase
    {

        public DbContextRideTests(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public async Task GetByID_Ride_ByDriver()
        {
            //ARRANGE
            //ACT
            var numberOfRide = await CarpoolDbContextSUT.Rides
                .Where(i => i.DriverId == UserSeeds.Romana_modrava.Id)
                .ToArrayAsync();

            //ASSERT
            Assert.Equal(RideSeeds.Olomouc_trinec, numberOfRide[0], RideEntity.RideEntityComparer);
        }

        [Fact]
        public async Task Update_Ride()
        {
            //ARRANGE
            var baseEntity = RideSeeds.Olomouc_trinec;
            var entity = baseEntity with
            {
                Start = "Brno_update",
                Destination = "Praha_update"
            };

            //ACT
            CarpoolDbContextSUT.Rides.Update(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Rides.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity, RideEntity.RideEntityComparer);
        }

        [Fact]
        public async Task AddNew_Ride()
        {
            //ARRANGE
            RideEntity entity = new(
                Start: "Znojmo",
                Destination: "Praha",
                DriverId: UserSeeds.Jan_novak.Id,
                CarId: CarSeeds.Skoda_superb.Id
                )
            {
                Id = Guid.Parse(input: "9A0DCDBC-456A-AAAF-86E6-BACF71896234"),
                DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Duration = new TimeSpan(3, 15, 1)
            };

            //ACT
                CarpoolDbContextSUT.Rides.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Rides.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntities, RideEntity.RideEntityComparer);
        }

        [Fact]
        public async Task Delete_Ride_PardubiceJihlava()
        {

            //ARRANGE
            RideEntity entity = new(
                Start: "Pardubice",
                Destination: "Jihlava",
                DriverId: UserSeeds.Romana_modrava.Id,
                CarId: CarSeeds.Suzuki_swift.Id
                )
           {
                Id = Guid.Parse(input: "28853EA8-3A93-4199-85EA-5790CAED6053"),
                DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Duration = new TimeSpan(1, 30, 0)
           };

            CarpoolDbContextSUT.Rides.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ACT
            CarpoolDbContextSUT.Rides.Remove(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            Assert.False(await CarpoolDbContextSUT.Rides.AnyAsync(i => i.Id == entity.Id));
        }
    }
}