namespace Aplicatie_Transporturi.Entities
{
    public class Delivery
    {
        public int Id { get; set; }

        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;

        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } = "Planned";

        public int? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
    }
}
