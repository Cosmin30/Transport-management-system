using Aplicatie_Transporturi.Entities;

public class Delivery
{
    public int Id { get; set; }
    public string PickupLocation { get; set; }
    public string DropoffLocation { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string Status { get; set; } = "Planned"; // Planned, InProgress, Completed

    public int? VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }

    public int? DriverId { get; set; }
    public Driver Driver { get; set; }
}