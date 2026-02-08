using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Aplicatie_Transporturi.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        
        public decimal TotalMaintenanceCost { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public int TotalKmDriven { get; set; }
        public decimal FuelConsumptionPer100Km { get; set; }
        
        public int UserId { get; set; }

        [ValidateNever]
        [JsonIgnore]    
        public AppUser? User { get; set; }
    }
}
