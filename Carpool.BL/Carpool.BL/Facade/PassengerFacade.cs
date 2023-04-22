using AutoMapper;
using Carpool.BL.Models;
using Carpool.DAL.Entities;
using Carpool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Carpool.BL.Facade;

public class PassengerFacade : CRUDFacade<PassengerEntity, PassengerListModel, PassengerModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public PassengerFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public new async Task<PassengerModel> SaveAsync(PassengerModel model)
    {
        await using var uow = _unitOfWorkFactory.Create();

        if (await this.IsCapacityFull(model)){
            throw new DbUpdateException("Car occupancy is full for this ride!");
        }
        if(model.Ride.Driver.Id == model.User.Id)
        {
            throw new DbUpdateException("You are in this ride as driver already!");
        }
        if (await IsInRideAlready(model,model.User.Id))
        {
            throw new DbUpdateException("You are in this ride already!");
        }

        var entity = await uow
            .GetRepository<PassengerEntity>()
            .InsertOrUpdateAsync(model, _mapper)
            .ConfigureAwait(false);
        await uow.CommitAsync();

        return (await GetAsync(entity.Id).ConfigureAwait(false))!;
    }

    private async Task<bool> IsCapacityFull(PassengerModel passenger)
    {
        var max_capacity = passenger.Ride.Car.Capacity;

        var passengers = await this.GetAsync();

        var current_ocupacy = passengers.Where(p => p.Ride == passenger.Ride).Count() + 1;

        if (current_ocupacy < max_capacity)
            return false;
        return true;
    }

    private async Task<bool> IsInRideAlready(PassengerModel passenger, Guid userId)
    {
        var passengers = await this.GetAsync();
        
        var current_Ride = passengers.Where(p => p.Ride == passenger.Ride);

        foreach(var psngr in current_Ride)
        {
            if(psngr.User.Id == userId)
            {
                return true;
            }
        }
        return false;
    }
}