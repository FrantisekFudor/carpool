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

public class RideFacadeTests : CRUDFacadeTestsBase
{
    private readonly RideFacade _rideFacade;

    public RideFacadeTests(ITestOutputHelper output) : base(output)
    {
        _rideFacade = new RideFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task GetById_DoesNotThrowAndEquals_FromSeeded_()
    {
        //ARRANGE
        var model = Mapper.Map<RideModel>(RideSeeds.Ostrava_cb);

        //ACT
        var returnedModel = await _rideFacade.GetAsync(model.Id);

        //ASSERT
        Assert.Equal(model, returnedModel! with {Car = null, Driver = null }, RideModel.RideModelComparer);
    }

    [Fact]
    public async Task Delete_DoesNotThrow_FromSeeded()
    {
        //ARRANGE
        var model = Mapper.Map<RideModel>(RideSeeds.Krumlov_praha);

        //ACT, ASSERT
        await _rideFacade.DeleteAsync(model);
    }

    [Fact]
    public async Task Update_DoesNotThrow_FromSeeded()
    {
        //ARRANGE
        var model = await _rideFacade.GetAsync(RideSeeds.Krumlov_praha.Id);
        
        if (model == null) 
            throw new System.NullReferenceException("Given ride not found");
        
        model.Destination = "Destination changed";

        //ACT, ASSERT
        await _rideFacade.SaveAsync(model);
    }

    [Fact]
    public async Task DeleteById_DoesNotThrow_FromSeeded()
    {
        //ARRANGE, ACT, ASSERT
        await _rideFacade.DeleteAsync(RideSeeds.Krumlov_praha.Id);
    }

    [Fact]
    public async Task Create_NonExist_RideItem()
    {
        var model = new RideModel
        (
            Start: "New Ride start",
            Destination: "New Ride stop",
            DepartureTime: DateTime.Now,
            Driver: Mapper.Map<UserListModel>(UserSeeds.Hubert_forman),
            Car: Mapper.Map<CarListModel>(CarSeeds.Honda_civic)
        );

        var _ = await _rideFacade.SaveAsync(model);
    }

    [Fact]
    public async Task Search_created_ride()
    {
        // ARRANGE
        var ride = new RideModel
        (
            Start: "FIT",
            Destination: "FSI",
            DepartureTime: DateTime.Now,
            Driver: Mapper.Map<UserListModel>(UserSeeds.Anastazie_bohata),
            Car: Mapper.Map<CarListModel>(CarSeeds.Renault_clio)
        );

        // ACT
        ride = await _rideFacade.SaveAsync(ride);
        Assert.NotNull(ride);

        var found_rides = await _rideFacade.SearchRides("FIT", "FSI");
        Assert.NotEmpty(found_rides);

        var found_ride = found_rides.Single(r => r.Id == ride.Id);
    }
}