using System;

#nullable enable

namespace Carpool.DAL.Entities
{
	public record UserEntity(string Name, string Surname) : IEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = Name;
		public string Surname { get; set; } = Surname;
		public string? PhotoUrl { get; set; }
		public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
		public ICollection<RideEntity> RidesAsDriver { get; set; } = new List<RideEntity>();
		public ICollection<PassengerEntity> RidesAsPassenger { get; set; } = new List<PassengerEntity>();

        private sealed record UserEntityEqualityComparer : IEqualityComparer<UserEntity>
        {
            public bool Equals(UserEntity? x, UserEntity? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.Name == y.Name && x.Surname == y.Surname && x.PhotoUrl == y.PhotoUrl;
            }

            public int GetHashCode(UserEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Name, obj.Surname, obj.PhotoUrl, obj.Cars, obj.RidesAsDriver, obj.RidesAsPassenger);
            }
        }

        public static IEqualityComparer<UserEntity> UserEntityComparer { get; } = new UserEntityEqualityComparer();
    }
}
