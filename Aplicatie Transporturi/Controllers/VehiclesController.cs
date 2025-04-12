namespace Aplicatie_Transporturi.Controllers
{
    using Aplicatie_Transporturi.Data;
    using Aplicatie_Transporturi.Entities;
    using Aplicatie_Transporturi.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
            return Ok(await _repo.GetVehiclesAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicle(Vehicle vehicle)
        {
            await _repo.AddVehicleAsync(vehicle);
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