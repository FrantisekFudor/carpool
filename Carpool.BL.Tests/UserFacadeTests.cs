using System.Linq;
using System.Threading.Tasks;
using Carpool.BL.Facade;
using Xunit;
using Xunit.Abstractions;
using Carpool.BL.Models;
using Carpool.Common.Tests;
using Carpool.DAL.Seeds;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Carpool.BL.Tests;

public class UserFacadeTests : CRUDFacadeTestsBase
{
    private readonly UserFacade _userFacade;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _userFacade = new UserFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task GetById_DoesNotThrowAndEqualsSeeded_FromSeeded()
    {
        //ARRANGE
        var model = Mapper.Map<UserModel>(UserSeeds.Romana_modrava);

        //ACT
        var returnedModel = await _userFacade.GetAsync(model.Id);

        //ASSERT
        Assert.Equal(model, returnedModel!, UserModel.UserModelComparer);
    }

    [Fact]
    public async Task GetAll_DoesNotThrowAndContainsSeeded_FromSeeded()
    {
        //ARRANGE
        var listModel = Mapper.Map<UserListModel>(UserSeeds.Romana_modrava);

        //ACT
        var returnedModel = await _userFacade.GetAsync();

        //ASSERT
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Delete_DoesNotThrow_FromSeeded()
    {
        //ARRANGE
        var model = Mapper.Map<UserModel>(UserSeeds.Romana_modrava);

        //ACT, ASSERT
        await _userFacade.DeleteAsync(model);
    }

    [Fact]
    public async Task Update_DoesNotThrow_FromSeeded()
    {
        //ARRANGE
        var model = Mapper.Map<UserModel>(UserSeeds.Romana_modrava);
        model.Surname = "Surname changed";

        //ACT, ASSERT
        await _userFacade.SaveAsync(model);
    }

    [Fact]
    public async Task Update_Surname_CheckUpdated_FromSeeded()
    {
        //ARRANGE
        var model = Mapper.Map<UserModel>(UserSeeds.Romana_modrava);
        model.Surname = "Surname changed";

        //ACT
        await _userFacade.SaveAsync(model);

        //ASSERT
        var returnedModel = await _userFacade.GetAsync(model.Id);
        Assert.Equal(model, returnedModel!, UserModel.UserModelComparer);
    }

    [Fact]
    public async Task DeleteById_DoesNotThrow_FromSeeded()
    {
        //ARRANGE, ACT, ASSERT
        await _userFacade.DeleteAsync(UserSeeds.Romana_modrava.Id);
    }

    [Fact]
    public async Task Create_NonExist_UserItem()
    {
        var model = new UserModel
        (
            Name: "New Name",
            Surname: "New Surname"
        );

        var _ = await _userFacade.SaveAsync(model);
    }


}