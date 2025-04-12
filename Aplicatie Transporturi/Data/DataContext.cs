namespace Aplicatie_Transporturi.Data
{
    using Microsoft.EntityFrameworkCore;
    using Aplicatie_Transporturi.Entities;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
    }
}