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

namespace Carpool.BL.Tests;

public class PassengerFacadeTests : CRUDFacadeTestsBase
{
    private readonly PassengerFacade _passengerFacade;
    private readonly RideFacade _rideFacade;
    private readonly UserFacade _userFacade;

    public PassengerFacadeTests(ITestOutputHelper output) : base(output)
    {
        _passengerFacade = new PassengerFacade(UnitOfWorkFactory, Mapper);
        _rideFacade = new RideFacade(UnitOfWorkFactory, Mapper);
        _userFacade = new UserFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task Update_DoesNotThrow_FromSeeded()
    {
        // ARRANGE
        var model = await _passengerFacade.GetAsync(PassengerSeeds.Passenger_1.Id);

        if (model == null)
            throw new System.NullReferenceException("Given ride not found");

        model.User = Mapper.Map<UserListModel>(UserSeeds.Alfred_rychly);

        //ACT, ASSERT
        await _passengerFacade.SaveAsync(model);
    }

    [Fact]
    public async Task Create_NonExist_Passenger()
    {
        var user = await _userFacade.GetAsyncList(UserSeeds.Hubert_forman.Id);
        Assert.NotNull(user);
        var ride = await _rideFacade.GetAsyncList(RideSeeds.Liberec_usti.Id);
        Assert.NotNull(ride);

        var model = new PassengerModel
        (
            User: user ?? throw new NullReferenceException(),
            Ride: ride ?? throw new NullReferenceException()
        );

        var _ = await _passengerFacade.SaveAsync(model);
    }
}