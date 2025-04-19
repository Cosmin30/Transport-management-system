using Aplicatie_Transporturi.Entities;
namespace Aplicatie_Transporturi.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetVehiclesByUserIdAsync(int userId);
        Task<Vehicle?> GetVehicleByIdAsync(int id);
        Task AddVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id);

    }
}