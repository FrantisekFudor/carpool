using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpool.BL;
using Carpool.BL.Facade;
using Carpool.BL.Models;
using Carpool.DAL.Factories;
using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.App.Services
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarEntity, CarListModel>();

            CreateMap<CarEntity, CarModel>()
                .ReverseMap()
                .ForMember(entity => entity.Owner, expression => expression.Ignore());

            CreateMap<PassengerEntity, PassengerListModel>();

            CreateMap<PassengerEntity, PassengerModel>()
                .ReverseMap()
                .ForMember(p => p.User, e => e.Ignore())
                .ForMember(p => p.Ride, e => e.Ignore());

            CreateMap<RideEntity, RideListModel>();

            CreateMap<RideEntity, RideModel>()
                .ReverseMap()
                .ForMember(i => i.Driver, expression => expression.Ignore())
                .ForMember(i => i.Car, expression => expression.Ignore())
                .ForMember(i => i.Passengers, expression => expression.Ignore());

            CreateMap<UserEntity, UserListModel>();

            CreateMap<UserEntity, UserModel>()
                .ReverseMap();
        }
    }
}
