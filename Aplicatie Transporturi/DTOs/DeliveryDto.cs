namespace Aplicatie_Transporturi.DTOs
{
    public class DeliveryDto
    {
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int? VehicleId { get; set; }
        public int? DriverId { get; set; }
    }
}
