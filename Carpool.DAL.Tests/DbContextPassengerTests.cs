using System;
using System.Linq;
using System.Threading.Tasks;
using Carpool.DAL.Seeds;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Carpool.DAL.Tests
{
    public class DbContextPassengerTests : DbContextTestsBase
    {

        public DbContextPassengerTests(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public async Task Delete_Passenger_HubertForman()
        {
            //Arrange
            PassengerEntity entity = new(
                 UserId: UserSeeds.Hubert_forman.Id,
                 RideId: RideSeeds.Krumlov_praha.Id
                 )
             {
                Id = Guid.Parse(input: "BA09D5E5-E872-45D2-9AD0-962FF1FF5C67")
            };
            CarpoolDbContextSUT.Passengers.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Passengers.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity);

            //Act
            CarpoolDbContextSUT.Passengers.Remove(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await CarpoolDbContextSUT.Passengers.AnyAsync(i => i.Id == entity.Id));
        }

        [Fact]
        public async Task AddNew_Passenger_Persisted()
        {
            //Arrange
            PassengerEntity entity = new(
                 UserId: UserSeeds.Hubert_forman.Id,
                 RideId: RideSeeds.Krumlov_praha.Id
                 )
            {
                Id = Guid.Parse(input: "ACC4CE38-0EDB-4221-A061-E7B35696440A")
            };

            //Act
            CarpoolDbContextSUT.Passengers.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Passengers.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntities);
        }
    }
}