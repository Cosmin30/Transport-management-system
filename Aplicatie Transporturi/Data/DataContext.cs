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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Delivery>()
                .Property(d => d.EstimatedCost)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Delivery>()
                .Property(d => d.ActualCost)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Delivery>()
                .Property(d => d.FuelCost)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Delivery>()
                .Property(d => d.Revenue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.TotalMaintenanceCost)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.FuelConsumptionPer100Km)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Driver>()
                .Property(d => d.TotalKmDriven)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Driver)
                .WithMany()
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Vehicle)
                .WithMany()
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.User)
                .WithMany(u => u.Deliveries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.User)
                .WithMany(u => u.Drivers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}