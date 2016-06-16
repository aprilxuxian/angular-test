using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iasset.FlightDashBoard.Domain.Constants;
using iasset.FlightDashBoard.Domain.Enums;

namespace iasset.FlightDashBoard.Models
{
    public class FlightModel : IValidatableObject
    {
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string FlightNumber { get; set; }
        public string FlightStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.Compare(ArrivalTime, DepartureTime, StringComparison.Ordinal) >= 0)
            {
                yield return new ValidationResult(Constant.ArrivalErrorMessage, new[] { "ArrivalTime" });
            }
        }
    }
}
