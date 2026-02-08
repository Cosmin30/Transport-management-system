using System.ComponentModel.DataAnnotations;

namespace Aplicatie_Transporturi.DTOs
{
    public class DeliveryDto
    {
        [Required(ErrorMessage = "Pickup location is required")]
        [StringLength(200, ErrorMessage = "Pickup location cannot exceed 200 characters")]
        public string PickupLocation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Dropoff location is required")]
        [StringLength(200, ErrorMessage = "Dropoff location cannot exceed 200 characters")]
        public string DropoffLocation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Scheduled date is required")]
        public DateTime ScheduledDate { get; set; }

        public int? VehicleId { get; set; }
        
        public int? DriverId { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }

        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = "Planned";

        [Range(0, double.MaxValue, ErrorMessage = "Estimated cost must be positive")]
        public decimal EstimatedCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Revenue must be positive")]
        public decimal Revenue { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Distance must be positive")]
        public int DistanceKm { get; set; }
    }
}
