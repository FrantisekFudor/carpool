using System;

#nullable enable

namespace Carpool.DAL.Entities
{
	public record CarEntity(string Manufacturer, string Type, Guid OwnerId) : IEntity
	{
		public Guid Id { get; set; }
		public string Manufacturer { get; set; } = Manufacturer;
		public string Type { get; set; } = Type;
		public DateTime DateOfFirstRegistration { get; set; }
		public string? PhotoUrl { get; set; }
		public int Capacity { get; set; }
		public UserEntity? Owner { get; set; }
		public Guid OwnerId { get; set; } = OwnerId;
    }
}
