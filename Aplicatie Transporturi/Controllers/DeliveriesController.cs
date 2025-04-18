using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aplicatie_Transporturi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryRepository _repo;

        public DeliveriesController(IDeliveryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            return Ok(await _repo.GetDeliveriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDeliveryById(int id)
        {
            var delivery = await _repo.GetDeliveryByIdAsync(id);
            if (delivery == null) return NotFound();
            return Ok(delivery);
        }

        [HttpPost]
        public async Task<ActionResult> AddDelivery(Delivery delivery)
        {
            await _repo.AddDeliveryAsync(delivery);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDelivery(int id, Delivery delivery)
        {
            if (id != delivery.Id) return BadRequest("ID mismatch");
            await _repo.UpdateDeliveryAsync(delivery);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDelivery(int id)
        {
            await _repo.DeleteDeliveryAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromQuery] string status)
        {
            await _repo.UpdateDeliveryStatusAsync(id, status);
            return Ok();
        }
    }
}
