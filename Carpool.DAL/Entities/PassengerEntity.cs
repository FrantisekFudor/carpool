using System;

namespace Carpool.DAL.Entities
{
	public record PassengerEntity(Guid UserId, Guid RideId) : IEntity
	{
		public Guid Id { get; set; }

		public Guid UserId { get; set; } = UserId;
		public UserEntity? User { get; set; }

		public Guid RideId { get; set; } = RideId;
		public RideEntity? Ride { get; set; }
    }
}
