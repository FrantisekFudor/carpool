using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Carpool.DAL;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models;

public record CarModel(
    string Manufacturer,
    string Type,
    DateTime DateOfFirstRegistration,
    int Capacity,
    UserListModel Owner
) : ModelBase

{
    public string Manufacturer { get; set; } = Manufacturer;
    public string Type { get; set; } = Type;
    public DateTime DateOfFirstRegistration { get; set; } = DateOfFirstRegistration;
    public string? PhotoUrl { get; set; }
    public int Capacity { get; set; } = Capacity;
    public UserListModel Owner { get; set; } = Owner;
    

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarEntity, CarModel>()
                .ReverseMap()
                .ForMember(entity => entity.Owner, expression => expression.Ignore());
        }
    }
    public static CarModel? Empty => new(string.Empty, string.Empty, DateTime.Now, 0, new UserListModel(string.Empty, string.Empty));
}