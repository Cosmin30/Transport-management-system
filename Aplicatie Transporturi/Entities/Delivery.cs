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

        public int? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
        public int UserId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public AppUser? User { get; set; }
    }

}
