namespace Aplicatie_Transporturi.DTOs
{
    public class DeliveryReportDto
    {
        public int TotalDeliveries { get; set; }
        public int CompletedDeliveries { get; set; }
        public int InProgressDeliveries { get; set; }
        public int PlannedDeliveries { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCosts { get; set; }
        public decimal TotalProfit { get; set; }
        public int TotalKmDriven { get; set; }
        public decimal AverageDeliveryProfit { get; set; }
    }

    public class DriverPerformanceDto
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public int TotalDeliveries { get; set; }
        public decimal TotalKmDriven { get; set; }
        public DateTime? LastDeliveryDate { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class VehicleUtilizationDto
    {
        public int VehicleId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int TotalDeliveriesAssigned { get; set; }
        public int TotalKmDriven { get; set; }
        public decimal TotalMaintenanceCost { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class MonthlyReportDto
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalDeliveries { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
