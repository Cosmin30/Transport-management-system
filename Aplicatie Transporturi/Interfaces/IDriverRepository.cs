public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetDriversAsync();
    Task AddDriverAsync(Driver driver);
    Task DeleteDriverAsync(int id);
}