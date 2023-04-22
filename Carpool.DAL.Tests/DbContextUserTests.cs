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
    public class DbContextUserTests : DbContextTestsBase
    {
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {

        }

        [Fact]
        public async Task AddNew_User()
        {
            //ARRANGE
            UserEntity entity = new(
                Name : "Tomáš",
                Surname : "Nový"
                )
            ;

            //ACT
            CarpoolDbContextSUT.Users.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users
                .SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity, UserEntity.UserEntityComparer);
        }


        [Fact]
        public async Task GetById_User()
        {
            //ARRANGE
            //ACT
            var entity = await CarpoolDbContextSUT.Users
                .SingleAsync(i => i.Id == UserSeeds.Kvetoslava_svizna.Id);

            //ASSERT
            Assert.Equal(UserSeeds.Kvetoslava_svizna, entity, UserEntity.UserEntityComparer);
        }

        [Fact]
        public async Task Update_User()
        {
            //ARRANGE
            var baseEntity = UserSeeds.Ota_toman;
            var entity =
                baseEntity with
                {
                    Name = "Jan_update",
                    Surname = "Novak_update",
                    PhotoUrl = "asdf_update"
                };

            //ACT
            CarpoolDbContextSUT.Users.Update(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //ASSERT
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity, UserEntity.UserEntityComparer);
        }

        [Fact]
        public async Task AddNew_User_Persisted()
        {
            //Arrange
            UserEntity entity = new(
                Name: "Josef",
                Surname: "Adalbert"
                )
            {
                Id = Guid.Parse("5AA19D60-39A0-4488-8D6F-CC58818BA877")
            };

            //Act
            CarpoolDbContextSUT.Users.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntities, UserEntity.UserEntityComparer);
        }


        [Fact]
        public async Task GetAll_Users_Contains_Something()
        {
            //Act
            var entities = await CarpoolDbContextSUT.Users.ToArrayAsync();

            //Assert
            Assert.True(entities.Length > 0);
        }

        [Fact]
        public async Task GetAll_Users_Contains_Seeds_Jan_Novak()
        {
            //Act
            var entities = await CarpoolDbContextSUT.Users.ToArrayAsync();

            //Assert
            var Jan_Novak = entities.SingleOrDefault(i => i.Id == UserSeeds.Jan_novak.Id);
            Assert.NotNull(Jan_Novak);
            Assert.Equal(UserSeeds.Jan_novak, Jan_Novak, UserEntity.UserEntityComparer);
        }

        [Fact]
        public async Task GetById_User_Jan_Novak_Retrieved()
        {
            //Act
            var entity = await CarpoolDbContextSUT.Users.SingleAsync(i=>i.Id == UserSeeds.Jan_novak.Id);

            //Assert
            Assert.Equal(UserSeeds.Jan_novak, entity, UserEntity.UserEntityComparer);
        }

        [Fact]
        public async Task Update_User_Persisted()
        {
            //Arrange
            var baseEntity = UserSeeds.Karel_polacek;
            var entity =
                baseEntity with
                {
                    Name = baseEntity + "Updated",
                    Surname = baseEntity + "Updated",
                    PhotoUrl = "qwertz"
                };

            //Act
            CarpoolDbContextSUT.Users.Update(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity, UserEntity.UserEntityComparer);
        }

        [Fact]
        public async Task Delete_User_JungmanDeleted()
        {
            //Arrange
            UserEntity entity = new(
                Name: "Josef",
                Surname: "Jungman"
                )
            {
                Id = Guid.Parse("7ADB0586-05EB-45CC-BED6-38B706D9884D"),
                PhotoUrl = "asdfghj"
            };
            CarpoolDbContextSUT.Users.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity, UserEntity.UserEntityComparer);

            //Act
            CarpoolDbContextSUT.Users.Remove(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await CarpoolDbContextSUT.Users.AnyAsync(i => i.Id == entity.Id));
        }

        
        [Fact]
        public async Task DeleteById_User_ZabranskyDeleted()
        {
            //Arrange
            UserEntity entity = new(
                Name: "Jaromir",
                Surname: "Zabransky"
                )
            {
                Id = Guid.Parse("84A941FB-B924-480D-9465-B26D2A91D8FA"),
                PhotoUrl = "yxcvbn"
            };
            CarpoolDbContextSUT.Users.Add(entity);
            await CarpoolDbContextSUT.SaveChangesAsync();
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntity, UserEntity.UserEntityComparer);

            //Act
            CarpoolDbContextSUT.Remove(
                CarpoolDbContextSUT.Users.Single(i => i.Id == entity.Id));
            await CarpoolDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await CarpoolDbContextSUT.Users.AnyAsync(i => i.Id == entity.Id));
        }
        
    }
}
