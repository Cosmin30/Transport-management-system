using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Aplicatie_Transporturi.Entities
{

    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        
        public int TotalDeliveriesCompleted { get; set; }
        public decimal TotalKmDriven { get; set; }
        public DateTime? LastDeliveryDate { get; set; }
        
        public int UserId { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public AppUser? User { get; set; } 
    }
}