using Aplicatie_Transporturi.DTOs;
using Aplicatie_Transporturi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplicatie_Transporturi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IDeliveryRepository _deliveryRepo;

        public ReportsController(IDeliveryRepository deliveryRepo)
        {
            _deliveryRepo = deliveryRepo;
        }

        [HttpGet("delivery-summary")]
        public async Task<ActionResult<DeliveryReportDto>> GetDeliverySummary()
        {
            var userId = User.GetUserId();
            var deliveries = await _deliveryRepo.GetDeliveriesByUserIdAsync(userId);

            var report = new DeliveryReportDto
            {
                TotalDeliveries = deliveries.Count(),
                CompletedDeliveries = deliveries.Count(d => d.Status == "Completed"),
                InProgressDeliveries = deliveries.Count(d => d.Status == "In Progress"),
                PlannedDeliveries = deliveries.Count(d => d.Status == "Planned"),
                TotalRevenue = deliveries.Sum(d => d.Revenue),
                TotalCosts = deliveries.Sum(d => d.ActualCost),
                TotalProfit = deliveries.Sum(d => d.Profit),
                TotalKmDriven = deliveries.Sum(d => d.DistanceKm),
                AverageDeliveryProfit = deliveries.Any() ? deliveries.Average(d => d.Profit) : 0
            };

            return Ok(report);
        }

        [HttpGet("driver-performance")]
        public async Task<ActionResult<IEnumerable<DriverPerformanceDto>>> GetDriverPerformance()
        {
            var userId = User.GetUserId();
            var drivers = await _deliveryRepo.GetDriversByUserIdAsync(userId);

            var performance = drivers.Select(d => new DriverPerformanceDto
            {
                DriverId = d.Id,
                DriverName = d.Name,
                TotalDeliveries = d.TotalDeliveriesCompleted,
                TotalKmDriven = d.TotalKmDriven,
                LastDeliveryDate = d.LastDeliveryDate,
                IsAvailable = d.IsAvailable
            });

            return Ok(performance);
        }

        [HttpGet("vehicle-utilization")]
        public async Task<ActionResult<IEnumerable<VehicleUtilizationDto>>> GetVehicleUtilization()
        {
            var userId = User.GetUserId();
            var vehicles = await _deliveryRepo.GetVehiclesByUserIdAsync(userId);
            var deliveries = await _deliveryRepo.GetDeliveriesByUserIdAsync(userId);

            var utilization = vehicles.Select(v => new VehicleUtilizationDto
            {
                VehicleId = v.Id,
                LicensePlate = v.LicensePlate,
                Model = v.Model,
                TotalDeliveriesAssigned = deliveries.Count(d => d.VehicleId == v.Id),
                TotalKmDriven = v.TotalKmDriven,
                TotalMaintenanceCost = v.TotalMaintenanceCost,
                IsAvailable = v.IsAvailable
            });

            return Ok(utilization);
        }

        [HttpGet("monthly")]
        public async Task<ActionResult<IEnumerable<MonthlyReportDto>>> GetMonthlyReport()
        {
            var userId = User.GetUserId();
            var deliveries = await _deliveryRepo.GetDeliveriesByUserIdAsync(userId);

            var monthlyData = deliveries
                .Where(d => d.Status == "Completed")
                .GroupBy(d => new { d.ScheduledDate.Year, d.ScheduledDate.Month })
                .Select(g => new MonthlyReportDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalDeliveries = g.Count(),
                    TotalRevenue = g.Sum(d => d.Revenue),
                    TotalProfit = g.Sum(d => d.Profit)
                })
                .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .Take(12);

            return Ok(monthlyData);
        }

        [HttpGet("financial-overview")]
        public async Task<ActionResult> GetFinancialOverview()
        {
            var userId = User.GetUserId();
            var deliveries = await _deliveryRepo.GetDeliveriesByUserIdAsync(userId);

            var overview = new
            {
                totalRevenue = deliveries.Sum(d => d.Revenue),
                totalCosts = deliveries.Sum(d => d.ActualCost),
                totalFuelCosts = deliveries.Sum(d => d.FuelCost),
                totalProfit = deliveries.Sum(d => d.Profit),
                profitMargin = deliveries.Sum(d => d.Revenue) > 0
                    ? (deliveries.Sum(d => d.Profit) / deliveries.Sum(d => d.Revenue)) * 100
                    : 0,
                averageRevenuePerDelivery = deliveries.Any() ? deliveries.Average(d => d.Revenue) : 0,
                averageCostPerKm = deliveries.Sum(d => d.DistanceKm) > 0
                    ? deliveries.Sum(d => d.ActualCost) / deliveries.Sum(d => d.DistanceKm)
                    : 0
            };

            return Ok(overview);
        }
    }
}
