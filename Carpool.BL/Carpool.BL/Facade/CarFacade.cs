using AutoMapper;
using Carpool.BL.Models;
using Carpool.DAL.Entities;
using Carpool.DAL.UnitOfWork;

namespace Carpool.BL.Facade;

public class CarFacade : CRUDFacade<CarEntity, CarListModel, CarModel>
{
    public CarFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper) {}
}