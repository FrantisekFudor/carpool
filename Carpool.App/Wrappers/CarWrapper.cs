using Carpool.BL.Models;
using System;
using Carpool.App.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Carpool.App.Wrappers
{
    public class CarWrapper : ModelWrapper<CarModel>
    {
        public CarWrapper(CarModel model)
               : base(model)
        {
        }

        public string? Manufacturer
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? Type
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime DateOfFirstRegistration
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public string? PhotoUrl
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public int Capacity
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public UserListModel? Owner
        {
            get => GetValue<UserListModel>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Manufacturer))
            {
                yield return new ValidationResult($"{nameof(Manufacturer)} is required", new[] { nameof(Manufacturer) });
            }
            if (string.IsNullOrWhiteSpace(Type))
            {
                yield return new ValidationResult($"{nameof(Type)} is required", new[] { nameof(Type) });
            }
            if (Capacity <= 0)
            {
                yield return new ValidationResult($"{nameof(Capacity)} is required", new[] { nameof(Capacity) });
            }
            if (Owner is null)
            {
                yield return new ValidationResult($"{nameof(Owner)} is required", new[] { nameof(Owner) });
            }

        }

        public static implicit operator CarWrapper(CarModel detailModel)
            => new(detailModel);

        public static implicit operator CarModel(CarWrapper wrapper)
            => wrapper.Model;
    }
}
