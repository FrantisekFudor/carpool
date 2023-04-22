using System;

#nullable enable

namespace Carpool.DAL.Entities
{
	public record RideEntity(string Start, string Destination, Guid DriverId, Guid CarId) : IEntity
	{
		public Guid Id { get; set; }
		public string Start { get; set; } = Start;
		public string Destination { get; set; } = Destination;
		public DateTime DepartureTime { get; set; }
		public TimeSpan? Duration { get; set; }
		public UserEntity? Driver { get; set; }
		public Guid DriverId { get; set; } = DriverId;
		public CarEntity? Car { get; set; }
		public Guid CarId { get; set; } = CarId;
		public ICollection<PassengerEntity> Passengers { get; set; } = new List<PassengerEntity>();

        private sealed record RideEntityEqualityComparer : IEqualityComparer<RideEntity>
        {
            public bool Equals(RideEntity? x, RideEntity? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.Start == y.Start && x.Destination == y.Destination && x.DepartureTime == y.DepartureTime && x.Duration == y.Duration && x.DriverId == y.DriverId && x.CarId == y.CarId;
            }

            public int GetHashCode(RideEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Start, obj.Destination, obj.Driver, obj.DriverId, obj.Car, obj.CarId, obj.Passengers);
            }
        }

        public static IEqualityComparer<RideEntity> RideEntityComparer { get; } = new RideEntityEqualityComparer();
    }
}
