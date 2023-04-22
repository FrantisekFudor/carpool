using Xunit;
using System.Linq;
using Xunit.Abstractions;
using Carpool.DAL.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Carpool.DAL.Seeds;
using System.Globalization;


namespace Carpool.DAL.Tests
{
    public class DbContextCarTests : DbContextTestsBase
    {
        public DbContextCarTests(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public async Task GetAll_Car_ByOwner()
        {
            //ARRANGE
            //ACT
            var numberOfCars = await CarpoolDbContextSUT.Cars
                .ToArrayAsync();

            //ASSERT
            Assert.Contains(CarSeeds.Honda_civic with { Owner = null }, numberOfCars);
        }

        [Fact]
        public async Task Update_Car()
        {
            //ARRANGE
            var baseEntity = CarSeeds.Bmw_m3;
            var entity = baseEntity with
            {
                Manufacturer = "Update" + baseEntity,
                Type = "Update" + baseEntity,
            };

            //ACT
            CarpoolDbContextSUT.Cars.Update(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task AddNew_Car_Persisted()
        {
            //ARRANGE
            CarEntity entity = new(
                Manufacturer: "Škoda",
                Type: "Kajak",
                OwnerId: UserSeeds.Jan_novak.Id
                )
            {
                Id = Guid.Parse(input: "9A0DCDBC-123A-4C54-86E6-BACF71896238"),
                DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                PhotoUrl = "asdfl",
                Capacity = 6
            };

            //ACT
            CarpoolDbContextSUT.Cars.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntities);
        }

        [Fact]
        public async Task GetAll_Cars_Contains_Something()
        {
            //Act
            var entities = await CarpoolDbContextSUT.Cars.ToArrayAsync();

            //Assert
            Assert.True(entities.Length > 0);
        }

        [Fact]
        public async Task GetAll_Cars_Contains_Seeds_Skoda_superb()
        {
            //Act
            var entities = await CarpoolDbContextSUT.Cars.ToArrayAsync();

            //Assert
            Assert.Contains(CarSeeds.Skoda_superb, entities);
        }

        [Fact]
        public async Task GetById_Car_Skoda_superb_Retrieved()
        {
            //Act
            var entity = await CarpoolDbContextSUT.Cars.SingleAsync(i=>i.Id == CarSeeds.Skoda_superb.Id);

            //Assert
            Assert.Equal(CarSeeds.Skoda_superb, entity);
        }


        [Fact]
        public async Task DeleteById_Car_SkodaSuperb()
        {
            //ARRANGE
            CarEntity entity = new(
                 Manufacturer: "Škoda",
                 Type: "Superb",
                 OwnerId: UserSeeds.Jan_novak.Id
                 )
            {
                Id = Guid.Parse(input: "E2925EA8-F122-488D-8377-4458E8CAEFEA"),
                DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                PhotoUrl = "asdf",
                Capacity = 5
            };

            CarpoolDbContextSUT.Cars.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ACT
            CarpoolDbContextSUT.Remove(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            Assert.False(await CarpoolDbContextSUT.Cars.AnyAsync(i => i.Id == entity.Id));
        }
    }
}
