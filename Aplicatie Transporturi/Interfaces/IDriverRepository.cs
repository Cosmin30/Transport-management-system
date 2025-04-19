// Aplicatie_Transporturi/Interfaces/IDriverRepository.cs
using Aplicatie_Transporturi.Entities;

namespace Aplicatie_Transporturi.Interfaces
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetDriversAsync();

       Task<IEnumerable<Driver>> GetDriversByUserIdAsync(int userId);

        Task<Driver?> GetDriverByIdAsync(int id);
        Task AddDriverAsync(Driver driver);
        Task UpdateDriverAsync(Driver driver);
        Task DeleteDriverAsync(int id);
    }
}
