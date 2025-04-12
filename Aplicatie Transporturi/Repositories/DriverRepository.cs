using Aplicatie_Transporturi.Data;
using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Aplicatie_Transporturi.Interfaces;
namespace Aplicatie_Transporturi.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DataContext _context;
        public DriverRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Driver>> GetDriversAsync() => await _context.Drivers.ToListAsync();
        public async Task AddDriverAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteDriverAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
                await _context.SaveChangesAsync();
            }
        }
    }
}