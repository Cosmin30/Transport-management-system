using Aplicatie_Transporturi.Data;
using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aplicatie_Transporturi.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DataContext _context;
        public DriverRepository(DataContext context) => _context = context;

        // all drivers
        public async Task<IEnumerable<Driver>> GetDriversAsync()
            => await _context.Drivers.ToListAsync();

        // only this user's drivers
        public async Task<IEnumerable<Driver>> GetDriversByUserIdAsync(int userId)
            => await _context.Drivers
                             .Where(d => d.UserId == userId)
                             .ToListAsync();

        public async Task<Driver?> GetDriverByIdAsync(int id)
            => await _context.Drivers.FindAsync(id);

        public async Task AddDriverAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDriverAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
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
