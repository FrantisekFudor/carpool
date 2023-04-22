using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models;

public record CarListModel(
    string Manufacturer,
    string Type, 
    int Capacity
) : ModelBase
{
    public string Manufacturer { get; set; } = Manufacturer;
    public string Type { get; set; } = Type;
    public int Capacity { get; set; } = Capacity;
    public string? PhotoUrl { get; set; }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarEntity, CarListModel>();
        }
    }

    public static CarListModel? Empty => new(string.Empty, string.Empty, 0);
}