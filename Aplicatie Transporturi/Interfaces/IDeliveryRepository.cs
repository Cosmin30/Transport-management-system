using Aplicatie_Transporturi.Entities;

namespace Aplicatie_Transporturi.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<IEnumerable<Delivery>> GetDeliveriesByUserIdAsync(int userId);
        Task<Delivery?> GetDeliveryByIdAsync(int id);
        Task AddDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryAsync(Delivery delivery);
        Task DeleteDeliveryAsync(int id);
        Task UpdateDeliveryStatusAsync(int deliveryId, string newStatus);
        
        Task<Vehicle?> GetVehicleByIdAsync(int id);
        Task<Driver?> GetDriverByIdAsync(int id);
        Task<IEnumerable<Driver>> GetDriversByUserIdAsync(int userId);
        Task<IEnumerable<Vehicle>> GetVehiclesByUserIdAsync(int userId);
    }
}