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

        public async Task<IEnumerable<Delivery>> GetDeliveriesAsync() => await _context.Deliveries
            .Include(d => d.Driver)
            .Include(d => d.Vehicle)
            .ToListAsync();

        public async Task AddDeliveryAsync(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeliveryStatusAsync(int deliveryId, string newStatus)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);
            if (delivery != null)
            {
                delivery.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}