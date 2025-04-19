using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Aplicatie_Transporturi.Extensions;
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
        public async Task<ActionResult> AddDelivery(Delivery delivery)
        {
            delivery.UserId = User.GetUserId();
            await _repo.AddDeliveryAsync(delivery);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDelivery(int id, Delivery delivery)
        {
            if (id != delivery.Id) return BadRequest("ID mismatch");
            delivery.UserId = User.GetUserId();
            await _repo.UpdateDeliveryAsync(delivery);
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
    }
}
