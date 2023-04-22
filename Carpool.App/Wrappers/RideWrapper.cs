using Carpool.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Carpool.App.Extensions;

namespace Carpool.App.Wrappers
{
    public class RideWrapper : ModelWrapper<RideModel>
    {
         public RideWrapper(RideModel model)
             : base(model)
         {

         }

         public string? Start
         {
             get => GetValue<string>();
             set => SetValue(value);
         }
         public string? Destination
         {
             get => GetValue<string>();
             set => SetValue(value);
         }
         public DateTime DepartureTime
         {
             get => GetValue<DateTime>();
             set => SetValue(value);
         }

         public TimeSpan? Duration
         {
             get => GetValue<TimeSpan>();
             set => SetValue(value);
         }
         public Guid DriverId
         {
             get => GetValue<Guid>();
             set => SetValue(value);
         }
         public Guid CarId
         {
             get => GetValue<Guid>();
             set => SetValue(value);
         }


         public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
         {
             if (string.IsNullOrWhiteSpace(Start))
             {
                 yield return new ValidationResult($"{nameof(Start)} is required", new[] { nameof(Start) });
             }

             if (string.IsNullOrWhiteSpace(Destination))
             {
                 yield return new ValidationResult($"{nameof(Destination)} is required", new[] { nameof(Destination) });
             }
             if (DepartureTime == default)
             {
                 yield return new ValidationResult($"{nameof(DepartureTime)} is required", new[] { nameof(DepartureTime) });
             }


         }

         public static implicit operator RideWrapper(RideModel detailModel)
             => new(detailModel);

         public static implicit operator RideModel(RideWrapper wrapper)
             => wrapper.Model;
}
    
}