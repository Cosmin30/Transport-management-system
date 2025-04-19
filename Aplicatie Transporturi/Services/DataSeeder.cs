using Aplicatie_Transporturi.Data;
using Aplicatie_Transporturi.Entities;
using Aplicatie_Transporturi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aplicatie_Transporturi.Services
{
    public class DataSeeder : IDataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedDataForUser(int userId)
        {
            // Verificăm dacă există deja datele
            var existingVehicles = await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();
            var existingDrivers = await _context.Drivers.Where(d => d.UserId == userId).ToListAsync();
            var existingDeliveries = await _context.Deliveries.Where(d => d.UserId == userId).ToListAsync();

            if (existingVehicles.Count >= 10 && existingDrivers.Count >= 10 && existingDeliveries.Count >= 10)
                return; // datele există deja

            var vehicles = new List<Vehicle>();
            var drivers = new List<Driver>();

            // Vehicule
            var models = new[] { "Duster", "Logan", "Sandero", "Transit", "Trafic", "Crafter", "Daily", "Ducato", "Sprinter", "Vivaro" };
            for (int i = 0; i < 10; i++)
            {
                var vehicle = new Vehicle
                {
                    UserId = userId,
                    LicensePlate = $"B-{i + 10}-ROM",
                    Model = models[i]
                };
                vehicles.Add(vehicle);
                _context.Vehicles.Add(vehicle);
            }

            // Șoferi
            var names = new[] { "Ion Popescu", "Vasile Ionescu", "Mihai Georgescu", "Andrei Stan", "Florin Dobre", "Paul Ene", "Cristian Oprea", "George Radu", "Emil Barbu", "Daniel Ilie" };
            for (int i = 0; i < 10; i++)
            {
                var driver = new Driver
                {
                    UserId = userId,
                    Name = names[i],
                    LicenseNumber = $"RODRV-{i + 1000}",
                    IsAvailable = true
                };
                drivers.Add(driver);
                _context.Drivers.Add(driver);
            }

            await _context.SaveChangesAsync(); // ca să avem ID-urile generate

            // Curse
            var locations = new[]
            {
                ("Cluj", "București"),
                ("Iași", "Constanța"),
                ("Timișoara", "Oradea"),
                ("Sibiu", "Brașov"),
                ("Arad", "Galați"),
                ("Suceava", "Craiova"),
                ("Botoșani", "Târgu Mureș"),
                ("Ploiești", "Alba Iulia"),
                ("Brăila", "Bistrița"),
                ("Focșani", "Deva")
            };

            for (int i = 0; i < 10; i++)
            {
                var delivery = new Delivery
                {
                    UserId = userId,
                    PickupLocation = locations[i].Item1,
                    DropoffLocation = locations[i].Item2,
                    ScheduledDate = DateTime.Now.AddDays(i + 1),
                    Status = "Planned",
                    DriverId = drivers[i].Id,
                    VehicleId = vehicles[i].Id
                };

                _context.Deliveries.Add(delivery);
            }

            await _context.SaveChangesAsync();
        }
    }
}
