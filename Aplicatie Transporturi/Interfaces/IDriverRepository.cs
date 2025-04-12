using Aplicatie_Transporturi.Entities;
namespace Aplicatie_Transporturi.Interfaces
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task AddDriverAsync(Driver driver);
        Task DeleteDriverAsync(int id);
    }
}