using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models;

public record UserModel(
    string Name,
    string Surname
) : ModelBase

{
    public string Name { get; set; } = Name;
    public string Surname { get; set; } = Surname;
    public string? PhotoUrl { get; set; }
    public ICollection<CarListModel> Cars { get; set; } = new List<CarListModel>();
    public ICollection<RideListModel> RidesAsDriver { get; set; } = new List<RideListModel>();
    public ICollection<PassengerListModel> RidesAsPassenger { get; set; } = new List<PassengerListModel>();

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ReverseMap()
                .ForMember(i => i.Cars, expression => expression.Ignore())
                .ForMember(i => i.RidesAsDriver, expression => expression.Ignore())
                .ForMember(i => i.RidesAsPassenger, expression => expression.Ignore()); 
        }
    }


    private sealed record UserModelEqualityComparer : IEqualityComparer<UserModel>
    {
        public bool Equals(UserModel? x, UserModel? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id.Equals(y.Id) && x.Name == y.Name && x.Surname == y.Surname && x.PhotoUrl == y.PhotoUrl;
        }

        public int GetHashCode(UserModel model)
        {
            return HashCode.Combine(model.Id, model.Name, model.Surname, model.PhotoUrl, model.Cars, model.RidesAsDriver, model.RidesAsPassenger);
        }
    }

    public static IEqualityComparer<UserModel> UserModelComparer { get; } = new UserModelEqualityComparer();

    public static UserModel? Empty => new(string.Empty, string.Empty);
}