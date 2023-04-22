using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models;

public record PassengerModel(
    UserListModel User,
    RideListModel Ride
) : ModelBase

{
    public UserListModel User { get; set; } = User;
    public RideListModel Ride { get; set; } = Ride;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PassengerEntity, PassengerModel>()
                .ReverseMap()
                .ForMember(p => p.User, e => e.Ignore())
                .ForMember(p => p.Ride, e => e.Ignore());
        }
    }
}

