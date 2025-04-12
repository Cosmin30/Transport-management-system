using Aplicatie_Transporturi.Entities;
using Microsoft.EntityFrameworkCore;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
}