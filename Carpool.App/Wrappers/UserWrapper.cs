using Carpool.BL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpool.App.Wrappers
{
    public class UserWrapper : ModelWrapper<UserModel>
    {
        public UserWrapper(UserModel? model)
            : base(model)
        {
        }

        public string? Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? Surname
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? PhotoUrl
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult($"{nameof(Name)} is required", new[] { nameof(Name) });
            }

            if (string.IsNullOrWhiteSpace(Surname))
            {
                yield return new ValidationResult($"{nameof(Surname)} is required", new[] { nameof(Surname) });
            }
        }

        public static implicit operator UserWrapper(UserModel? detailModel)
            => new(detailModel);

        public static implicit operator UserModel(UserWrapper wrapper)
            => wrapper.Model;
    }
}