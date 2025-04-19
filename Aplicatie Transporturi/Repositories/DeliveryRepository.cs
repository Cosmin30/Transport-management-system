using Aplicatie_Transporturi.Data;
using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aplicatie_Transporturi.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DataContext _context;
        public DeliveryRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Delivery>> GetDeliveriesByUserIdAsync(int userId)
        {
            return await _context.Deliveries
                .Where(d => d.UserId == userId)
                .Include(d => d.Driver)
                .Include(d => d.Vehicle)
                .ToListAsync();
        }

        public async Task<Delivery?> GetDeliveryByIdAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.Driver)
                .Include(d => d.Vehicle)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddDeliveryAsync(Delivery delivery)
        {
            delivery.ScheduledDate = DateTime.SpecifyKind(delivery.ScheduledDate, DateTimeKind.Utc);
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeliveryAsync(Delivery delivery)
        {
            var existing = await _context.Deliveries.FindAsync(delivery.Id);
            if (existing == null) return;
            _context.Entry(existing).CurrentValues.SetValues(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeliveryAsync(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null) return;
            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeliveryStatusAsync(int deliveryId, string newStatus)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);
            if (delivery == null) return;
            delivery.Status = newStatus;
            await _context.SaveChangesAsync();
        }
    }
}