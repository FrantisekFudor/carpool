using System.Linq;
using System.Threading.Tasks;
using Carpool.BL.Facade;
using Xunit;
using Xunit.Abstractions;
using Carpool.BL.Models;
using Carpool.Common.Tests;
using Carpool.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;

namespace Carpool.BL.Tests;

public class CarFacadeTests : CRUDFacadeTestsBase
{
    private readonly CarFacade _carFacade;

    public CarFacadeTests(ITestOutputHelper output) : base(output)
    {
        _carFacade = new CarFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task GetAll_Single_Bmw_m3_Seeded()
    {
        //ARRANGE
        var cars = await _carFacade.GetAsync();
        var car = cars.Single(i => i.Id == CarSeeds.Honda_civic.Id);
        
        //ASSERT
        Assert.Equal(Mapper.Map<CarListModel>(CarSeeds.Honda_civic), car);
    }

    [Fact]
    public async Task GetAll_DoesNotThrowAndContainsSeeded_FromSeeded()
    {
        //ARRANGE
        var listModel = Mapper.Map<CarListModel>(CarSeeds.Honda_civic);

        //ACT
        var returnedModel = await _carFacade.GetAsync();

        //ASSERT
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Delete_DoesNotThrow_FromSeeded()
    {
        //ARRANGE
        var model = Mapper.Map<CarModel>(CarSeeds.Honda_civic);

        //ACT, ASSERT
        await _carFacade.DeleteAsync(model);
    }

    [Fact]
    public async Task Update_DoesNotThrow_FromSeeded()
    {
        //ARRANGE
        var model = await _carFacade.GetAsync(CarSeeds.Honda_civic.Id);
        if (model == null)
            throw new System.NullReferenceException("Given car not found");

        model.Manufacturer = "Manufacturer changed";

        //ACT, ASSERT
        await _carFacade.SaveAsync(model);
    }

    [Fact]
    public async Task DeleteById_DoesNotThrow_FromSeeded()
    {
        //ARRANGE, ACT, ASSERT
        await _carFacade.DeleteAsync(CarSeeds.Honda_civic.Id);
    }

    [Fact]
    public async Task Create_WithNonExistingCarItem()
    {
        var model = new CarModel
        (
            Manufacturer: "Test Manufacturer 1",
            Type: "Test Type 1",
            DateOfFirstRegistration: DateTime.Now,
            Capacity: 1,
            Owner: Mapper.Map<UserListModel>(UserSeeds.Romana_modrava)
        );

        var _ = await _carFacade.SaveAsync(model);
    }
}