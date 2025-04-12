using Aplicatie_Transporturi.Entities;
namespace Aplicatie_Transporturi.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task AddVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id);
    }
}