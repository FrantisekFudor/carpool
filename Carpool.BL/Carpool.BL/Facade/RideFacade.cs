using AutoMapper;
using Carpool.BL.Models;
using Carpool.DAL.Entities;
using Carpool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Carpool.BL.Facade;

public class RideFacade : CRUDFacade<RideEntity, RideListModel, RideModel>
{    
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public RideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper) 
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public async Task<int> GetRestCapacityAsync(RideListModel ride)
    {
        return await GetRestCapacityAsync(await this.GetAsync(ride.Id));
    }

    public async Task<int> GetRestCapacityAsync(RideModel ride)
    {
        ride = await this.GetAsync(ride.Id);
        if (ride == null)
            throw new KeyNotFoundException("Key not found. Ride does not exist.");

        var current_ocupacy = ride.Passengers.Count() + 1;
        return current_ocupacy;
    }

    public async Task<IEnumerable<RideListModel>> SearchRides(string start = "", string destination = "", DateTime departureTime = default)
    {
        var rides = await this.GetAsync();
        if(start != "")
        {
            var selection = rides.Where(ride => ride.Start.ToLower() == start.ToLower());
            rides = selection;
        }
        if(destination != "")
        {
            var selection = rides.Where(ride => ride.Destination.ToLower() == destination.ToLower());
            rides = selection;
        }
        if(departureTime != default)
        {
            var selection = rides.Where(ride => ride.DepartureTime.Date == departureTime.Date);
            rides = selection;
        }

        return rides;

    }
}