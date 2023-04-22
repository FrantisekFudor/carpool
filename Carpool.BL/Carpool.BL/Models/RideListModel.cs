using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models;

public record RideListModel(
    string Start,
    string Destination,
    DateTime DepartureTime,
    UserListModel Driver,
    CarListModel Car
    ) : ModelBase
{
    public string Start { get; set; } = Start;
    public string Destination { get; set; } = Destination;
    public DateTime DepartureTime { get; set; } = DepartureTime;
    public UserListModel Driver { get; set; } = Driver;
    public CarListModel Car { get; set; } = Car;
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RideEntity, RideListModel>();
        }
    }
}