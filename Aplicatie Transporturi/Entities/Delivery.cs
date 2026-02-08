using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Aplicatie_Transporturi.Entities
{
    public class Delivery
    {
        public int Id { get; set; }

        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;

        private DateTime _scheduledDate;
        public DateTime ScheduledDate
        {
            get => _scheduledDate;
            set => _scheduledDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public string Status { get; set; } = "Planned";

        public double? CurrentLatitude { get; set; }
        public double? CurrentLongitude { get; set; }
        public DateTime? LastLocationUpdate { get; set; }

        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
        public decimal FuelCost { get; set; }
        public decimal Revenue { get; set; }
        
        public string? Notes { get; set; }
        public int DistanceKm { get; set; }

        public int? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
        public int UserId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public AppUser? User { get; set; }

        public decimal Profit => Revenue - ActualCost;
    }

}
