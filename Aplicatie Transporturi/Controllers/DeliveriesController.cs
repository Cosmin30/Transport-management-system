namespace Aplicatie_Transporturi.Controllers
{
    using Aplicatie_Transporturi.Entities;
    using Aplicatie_Transporturi.Interfaces;
    using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult> AddDelivery(Delivery delivery)
        {
            await _repo.AddDeliveryAsync(delivery);
            return Ok();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromQuery] string status)
        {
            await _repo.UpdateDeliveryStatusAsync(id, status);
            return Ok();
        }
    }
}
