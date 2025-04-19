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
            var userId = User.GetUserId();
            return Ok(await _repo.GetDriversByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver(Driver driver)
        {
            driver.UserId = User.GetUserId();
            await _repo.AddDriverAsync(driver);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var driver = await _repo.GetDriverByIdAsync(id);
            if (driver == null) return NotFound();
            return Ok(driver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, Driver driver)
        {
            if (id != driver.Id) return BadRequest("ID mismatch");
            driver.UserId = User.GetUserId();
            await _repo.UpdateDriverAsync(driver);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            await _repo.DeleteDriverAsync(id);
            return Ok();
        }
    }
}
