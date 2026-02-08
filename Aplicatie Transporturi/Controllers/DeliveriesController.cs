using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Aplicatie_Transporturi.Extensions;
using Aplicatie_Transporturi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplicatie_Transporturi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryRepository _repo;
        public DeliveriesController(IDeliveryRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            var userId = User.GetUserId();
            var deliveries = await _repo.GetDeliveriesByUserIdAsync(userId);
            return Ok(deliveries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var delivery = await _repo.GetDeliveryByIdAsync(id);
            if (delivery == null) return NotFound();
            return Ok(delivery);
        }

        [HttpPost]
        public async Task<ActionResult> AddDelivery(DeliveryDto deliveryDto)
        {
            var delivery = new Delivery
            {
                PickupLocation = deliveryDto.PickupLocation,
                DropoffLocation = deliveryDto.DropoffLocation,
                ScheduledDate = deliveryDto.ScheduledDate,
                Status = deliveryDto.Status,
                VehicleId = deliveryDto.VehicleId,
                DriverId = deliveryDto.DriverId,
                Notes = deliveryDto.Notes,
                DistanceKm = deliveryDto.DistanceKm,
                EstimatedCost = deliveryDto.EstimatedCost,
                Revenue = deliveryDto.Revenue,
                ActualCost = 0,
                FuelCost = 0,   
                UserId = User.GetUserId()
            };
            
            if (delivery.VehicleId.HasValue)
            {
                var vehicle = await _repo.GetVehicleByIdAsync(delivery.VehicleId.Value);
                if (vehicle == null || !vehicle.IsAvailable)
                    return BadRequest("Selected vehicle is not available");
            }

            if (delivery.DriverId.HasValue)
            {
                var driver = await _repo.GetDriverByIdAsync(delivery.DriverId.Value);
                if (driver == null || !driver.IsAvailable)
                    return BadRequest("Selected driver is not available");
            }

            await _repo.AddDeliveryAsync(delivery);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDelivery(int id, DeliveryDto deliveryDto)
        {
            var existingDelivery = await _repo.GetDeliveryByIdAsync(id);
            if (existingDelivery == null) return NotFound();
            existingDelivery.PickupLocation = deliveryDto.PickupLocation;
            existingDelivery.DropoffLocation = deliveryDto.DropoffLocation;
            existingDelivery.ScheduledDate = deliveryDto.ScheduledDate;
            existingDelivery.Status = deliveryDto.Status;
            existingDelivery.VehicleId = deliveryDto.VehicleId;
            existingDelivery.DriverId = deliveryDto.DriverId;
            existingDelivery.Notes = deliveryDto.Notes;
            existingDelivery.DistanceKm = deliveryDto.DistanceKm;
            existingDelivery.EstimatedCost = deliveryDto.EstimatedCost;
            existingDelivery.Revenue = deliveryDto.Revenue;
            existingDelivery.UserId = User.GetUserId();

            await _repo.UpdateDeliveryAsync(existingDelivery);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDelivery(int id)
        {
            await _repo.DeleteDeliveryAsync(id);
            return Ok();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            await _repo.UpdateDeliveryStatusAsync(id, newStatus);
            return Ok();
        }

        [HttpPatch("{id}/location")]
        public async Task<ActionResult> UpdateLocation(int id, [FromBody] LocationUpdateDto locationDto)
        {
            var delivery = await _repo.GetDeliveryByIdAsync(id);
            if (delivery == null) return NotFound();

            delivery.CurrentLatitude = locationDto.Latitude;
            delivery.CurrentLongitude = locationDto.Longitude;
            delivery.LastLocationUpdate = DateTime.UtcNow;

            await _repo.UpdateDeliveryAsync(delivery);
            return Ok();
        }

        [HttpGet("{id}/location")]
        public async Task<ActionResult> GetLocation(int id)
        {
            var delivery = await _repo.GetDeliveryByIdAsync(id);
            if (delivery == null) return NotFound();

            return Ok(new
            {
                latitude = delivery.CurrentLatitude,
                longitude = delivery.CurrentLongitude,
                lastUpdate = delivery.LastLocationUpdate
            });
        }

        [HttpPost("{id}/complete")]
        public async Task<ActionResult> CompleteDelivery(int id, [FromBody] decimal actualCost)
        {
            var delivery = await _repo.GetDeliveryByIdAsync(id);
            if (delivery == null) return NotFound();

            delivery.Status = "Completed";
            delivery.ActualCost = actualCost;

            if (delivery.DriverId.HasValue)
            {
                var driver = await _repo.GetDriverByIdAsync(delivery.DriverId.Value);
                if (driver != null)
                {
                    driver.TotalDeliveriesCompleted++;
                    driver.TotalKmDriven += delivery.DistanceKm;
                    driver.LastDeliveryDate = DateTime.UtcNow;
                }
            }

            if (delivery.VehicleId.HasValue)
            {
                var vehicle = await _repo.GetVehicleByIdAsync(delivery.VehicleId.Value);
                if (vehicle != null)
                {
                    vehicle.TotalKmDriven += delivery.DistanceKm;
                    delivery.FuelCost = (delivery.DistanceKm / 100m) * vehicle.FuelConsumptionPer100Km;
                }
            }

            await _repo.UpdateDeliveryAsync(delivery);
            return Ok(new { message = "Delivery completed successfully", profit = delivery.Profit });
        }
    }
}
