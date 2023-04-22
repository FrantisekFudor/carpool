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
using System.Globalization;

namespace Carpool.BL.Tests;

public class CollectionsTests : CRUDFacadeTestsBase
{
    private readonly CarFacade _carFacade;
    private readonly UserFacade _userFacade;
    private readonly RideFacade _rideFacade;
    private readonly PassengerFacade _passengerFacade;

    public CollectionsTests(ITestOutputHelper output) : base(output)
    {
        _carFacade = new CarFacade(UnitOfWorkFactory, Mapper);
        _userFacade = new UserFacade(UnitOfWorkFactory, Mapper);
        _rideFacade = new RideFacade(UnitOfWorkFactory, Mapper);
        _passengerFacade = new PassengerFacade(UnitOfWorkFactory, Mapper);
    }

    [Fact]
    public async Task Add_Car_To_User()
    {
        //ARRANGE
        var new_user = new UserModel("Karel", "M�d�lek") { PhotoUrl = "asdf"};
        var new_user_with_id = await _userFacade.SaveAsync(new_user);
        UserListModel? new_user_list_model = await _userFacade.GetAsyncList(new_user_with_id.Id);

        var new_car = new CarModel(
            Manufacturer: "Ford",
            Type: "T",
            DateOfFirstRegistration: DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
            Capacity: 5,
            Owner: new_user_list_model ?? throw new NullReferenceException());

        // ACT
        var cars = await _carFacade.SaveAsync(new_car);
        var final_user = await _userFacade.GetAsync(new_user_with_id.Id);

        //ASSERT
        Assert.Equal(0, new_user_with_id.Cars.Count);
        Assert.Equal(1, final_user?.Cars.Count);
    }

    [Fact]
    public async Task Add_Ride_To_User()
    {
        //ARRANGE
        UserModel? pavel_orig_detail = await _userFacade.GetAsync(UserSeeds.Pavel_zavodnik.Id);
        UserListModel? pavel_orig_list = await _userFacade.GetAsyncList(UserSeeds.Pavel_zavodnik.Id);

        Assert.NotEmpty(pavel_orig_detail?.Cars);
        if (pavel_orig_detail == null)
            throw new NullReferenceException();
        
        CarListModel? my_car = await _carFacade.GetAsyncList(pavel_orig_detail.Cars.ToArray()[0].Id);

        var new_ride = new RideModel(
            Start: "Brno",
            Destination: "Viden",
            DepartureTime: DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
            Driver: pavel_orig_list ?? throw new NullReferenceException(),
            Car: my_car ?? throw new NullReferenceException());

        // ACT
        var rides = await _rideFacade.SaveAsync(new_ride);
        var pavel_final_detail = await _userFacade.GetAsync(pavel_orig_detail.Id);

        // ASSERT
        var orig_rides_length = pavel_orig_detail?.RidesAsDriver.Count;
        Assert.True(orig_rides_length >= 0);
        Assert.Equal(orig_rides_length + 1, pavel_final_detail?.RidesAsDriver.Count);
    }

    [Fact]
    public async Task Add_Passenger_To_Ride()
    {
        //ARRANGE
        var new_user_detail = new UserModel(
            Name: "King",
            Surname: "George");
        new_user_detail = await _userFacade.SaveAsync(new_user_detail);
        var new_user_list = await _userFacade.GetAsyncList(new_user_detail.Id);

        var ride_orig_detail = await _rideFacade.GetAsync(RideSeeds.Brno_praha.Id);
        var ride_orig_list = await _rideFacade.GetAsyncList(RideSeeds.Brno_praha.Id);

        var new_passenger = new PassengerModel(
            User: new_user_list ?? throw new NullReferenceException(),
            Ride: ride_orig_list ?? throw new NullReferenceException());

        // ACT
        var passengers_orig = await _passengerFacade.GetAsync();
        var new_passenger_final = await _passengerFacade.SaveAsync(new_passenger);
        var passengers_final = await _passengerFacade.GetAsync();

        var new_user_after = await _userFacade.GetAsync(new_user_detail.Id);
        var ride_after = await _rideFacade.GetAsync(RideSeeds.Brno_praha.Id);

        // ASSERT
        Assert.NotNull(await _passengerFacade.GetAsync(new_passenger_final.Id));
        if (ride_orig_detail == null || ride_after == null || new_user_after == null)
            throw new NullReferenceException();
        Assert.True(new_user_after.RidesAsPassenger.Count() > 0);
        Assert.Equal(ride_orig_detail.Passengers.Count() + 1, ride_after.Passengers.Count());
    }

    [Fact]
    public async Task Add_Passenger_To_Ride_Over_Capacity()
    {
        //ARRANGE
        var driver = await _userFacade.GetAsyncList(UserSeeds.Alfred_rychly.Id);
        Assert.NotNull(driver);

        var new_car = new CarModel(
            Manufacturer: "Tesla",
            Type: "Y",
            DateOfFirstRegistration: DateTime.ParseExact("01.01.2001 12:00", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
            Capacity: 2,
            Owner: driver ?? throw new NullReferenceException());
        new_car = await _carFacade.SaveAsync(new_car);
        Assert.NotNull(new_car);
        var new_car_list = await _carFacade.GetAsyncList(new_car.Id);
        Assert.NotNull(new_car_list);

        var new_ride = new RideModel(
            Start: "FIT",
            Destination: "FEKT",
            DepartureTime: DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
            Driver: driver,
            Car: new_car_list ?? throw new NullReferenceException());

        new_ride = await _rideFacade.SaveAsync(new_ride);
        Assert.NotNull(new_ride);
        var new_ride_list = await _rideFacade.GetAsyncList(new_ride.Id);
        Assert.NotNull(new_ride_list);

        // ACT + ASSERT
        var user_1 = await _userFacade.GetAsyncList(UserSeeds.Jan_novak.Id);
        var user_2 = await _userFacade.GetAsyncList(UserSeeds.Anastazie_bohata.Id);

        var passenger_1 = new PassengerModel(
            User: user_1 ?? throw new NullReferenceException(),
            Ride: new_ride_list ?? throw new NullReferenceException());

        var passenger_2 = new PassengerModel(
            User: user_2 ?? throw new NullReferenceException(),
            Ride: new_ride_list);

        var rest_capacity = await _rideFacade.GetRestCapacityAsync(new_ride_list);
        Assert.Equal(1, rest_capacity);

        passenger_1 = await _passengerFacade.SaveAsync(passenger_1);

        rest_capacity = await _rideFacade.GetRestCapacityAsync(new_ride_list);
        Assert.Equal(0, rest_capacity);

        try
        {
            passenger_2 = await _passengerFacade.SaveAsync(passenger_2);
            Assert.True(false, "Assert Fail");
        }
        catch (DbUpdateException) { }
    }
}