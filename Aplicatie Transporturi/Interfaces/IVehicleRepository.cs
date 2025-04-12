using Aplicatie_Transporturi.Entities;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    Task AddVehicleAsync(Vehicle vehicle);
    Task DeleteVehicleAsync(int id);
}