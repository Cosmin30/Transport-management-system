namespace Aplicatie_Transporturi.Controllers
{
    using Aplicatie_Transporturi.Entities;
    using Aplicatie_Transporturi.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverRepository _repo;

        public DriversController(IDriverRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return Ok(await _repo.GetDriversAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddDriver(Driver driver)
        {
            await _repo.AddDriverAsync(driver);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver(int id)
        {
            await _repo.DeleteDriverAsync(id);
            return Ok();
        }
    }
}