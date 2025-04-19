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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _repo;

        public VehiclesController(IVehicleRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var userId = User.GetUserId();
            return Ok(await _repo.GetVehiclesByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicle(Vehicle vehicle)
        {
            vehicle.UserId = User.GetUserId();
            await _repo.AddVehicleAsync(vehicle);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _repo.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id) return BadRequest("ID mismatch");
            vehicle.UserId = User.GetUserId();
            await _repo.UpdateVehicleAsync(vehicle);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            await _repo.DeleteVehicleAsync(id);
            return Ok();
        }
    }
}
