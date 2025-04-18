using Aplicatie_Transporturi.Entities;

namespace Aplicatie_Transporturi.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<IEnumerable<Delivery>> GetDeliveriesAsync();
        Task<Delivery?> GetDeliveryByIdAsync(int id);
        Task AddDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryAsync(Delivery delivery);
        Task DeleteDeliveryAsync(int id);
        Task UpdateDeliveryStatusAsync(int deliveryId, string newStatus);
    }
}
