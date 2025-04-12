using Aplicatie_Transporturi.Data;
using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aplicatie_Transporturi.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _context;
        public VehicleRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync() => await _context.Vehicles.ToListAsync();
        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}