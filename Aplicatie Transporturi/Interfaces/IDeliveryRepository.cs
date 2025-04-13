using Aplicatie_Transporturi.Entities;

namespace Aplicatie_Transporturi.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<IEnumerable<Delivery>> GetDeliveriesAsync();
        Task AddDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryStatusAsync(int deliveryId, string newStatus);
    }
}