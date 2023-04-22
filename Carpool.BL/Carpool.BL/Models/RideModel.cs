using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models;

public record RideModel(
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
    public TimeSpan? Duration { get; set; }
    public UserListModel Driver { get; set; } = Driver;
    public CarListModel Car { get; set; } = Car;
    public ICollection<PassengerListModel> Passengers { get; set; } = new List<PassengerListModel>();

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RideEntity, RideModel>()
                .ReverseMap()
                .ForMember(i => i.Driver, expression => expression.Ignore())
                .ForMember(i => i.Car, expression => expression.Ignore())
                .ForMember(i => i.Passengers, expression => expression.Ignore());
        }
    }

    private sealed record RideModelEqualityComparer : IEqualityComparer<RideModel>
    {
        public bool Equals(RideModel? x, RideModel? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id.Equals(y.Id) && x.Start == y.Start && x.Destination == y.Destination && x.DepartureTime == y.DepartureTime && x.Duration == y.Duration && x.Driver == y.Driver && x.Car == y.Car;
        }

        public int GetHashCode(RideModel model)
        {
            return HashCode.Combine(model.Id, model.Start, model.Destination, model.Driver, model.Car, model.Passengers);
        }
    }

    public static IEqualityComparer<RideModel> RideModelComparer { get; } = new RideModelEqualityComparer();

    public static RideModel? Empty => new(string.Empty, string.Empty, DateTime.Now, UserListModel.Empty, CarListModel.Empty);
}