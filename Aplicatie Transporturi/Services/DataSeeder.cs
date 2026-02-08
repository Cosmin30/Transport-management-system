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
            var existingVehicles = await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();
            var existingDrivers = await _context.Drivers.Where(d => d.UserId == userId).ToListAsync();
            var existingDeliveries = await _context.Deliveries.Where(d => d.UserId == userId).ToListAsync();

            if (existingVehicles.Count >= 10 && existingDrivers.Count >= 10 && existingDeliveries.Count >= 20)
                return; 
            
            var rand = new Random(); 

            var vehicles = new List<Vehicle>();
            var drivers = new List<Driver>();

            var models = new[] { 
                "Dacia Duster", "Dacia Logan", "Renault Master", "Ford Transit", 
                "Renault Trafic", "VW Crafter", "Iveco Daily", "Fiat Ducato", 
                "Mercedes Sprinter", "Opel Vivaro" 
            };
            
            for (int i = 0; i < 10; i++)
            {
                var vehicle = new Vehicle
                {
                    UserId = userId,
                    LicensePlate = $"B-{rand.Next(10, 99)}-ABC",
                    Model = models[i],
                    Year = rand.Next(2018, 2024),
                    IsAvailable = i < 7, 
                    
                    TotalKmDriven = rand.Next(50000, 200000),
                    TotalMaintenanceCost = rand.Next(2000, 10000),
                    FuelConsumptionPer100Km = (decimal)(rand.Next(25, 45) + rand.NextDouble()), 
                    LastMaintenanceDate = DateTime.UtcNow.AddDays(-rand.Next(10, 90))
                };
                vehicles.Add(vehicle);
                _context.Vehicles.Add(vehicle);
            }

            var names = new[] { 
                "Ion Popescu", "Vasile Ionescu", "Mihai Georgescu", "Andrei Stan", 
                "Florin Dobre", "Paul Ene", "Cristian Oprea", "George Radu", 
                "Emil Barbu", "Daniel Ilie" 
            };
            
            for (int i = 0; i < 10; i++)
            {
                var driver = new Driver
                {
                    UserId = userId,
                    Name = names[i],
                    LicenseNumber = $"RO-DRV-{rand.Next(100000, 999999)}",
                    IsAvailable = i < 7, 
                    
                    TotalDeliveriesCompleted = rand.Next(50, 200),
                    TotalKmDriven = rand.Next(50000, 150000),
                    LastDeliveryDate = DateTime.UtcNow.AddDays(-rand.Next(1, 30))
                };
                drivers.Add(driver);
                _context.Drivers.Add(driver);
            }

            await _context.SaveChangesAsync(); 

            var locations = new[]
            {
                ("București, Str. Libertății 10", "Cluj-Napoca, Str. Unirii 25", 450),
                ("Iași, Piața Unirii 5", "Constanța, Bd. Mamaia 120", 380),
                ("Timișoara, Bd. Revoluției 8", "Oradea, Str. Republicii 30", 160),
                ("Sibiu, Piața Mare 1", "Brașov, Str. Republicii 15", 140),
                ("Arad, Bd. Revoluției 60", "Galați, Str. Domnească 10", 520),
                ("Suceava, Str. Ștefan cel Mare 2", "Craiova, Calea București 50", 580),
                ("Botoșani, Str. Calea Națională 12", "Târgu Mureș, Piața Trandafirilor 8", 310),
                ("Ploiești, Bd. Republicii 20", "Alba Iulia, Str. Horea 5", 250),
                ("Brăila, Str. Galați 45", "Bistrița, Piața Centrală 3", 420),
                ("Focșani, Str. Unirii 18", "Deva, Bd. Decebal 22", 380),
                ("București, Pipera", "Pitești, Centru", 120),
                ("Cluj, Mănăștur", "Sibiu, Centru Vechi", 200),
                ("Timișoara, Campus", "Arad, Gara Nord", 60),
                ("Iași, Copou", "Suceava, Burdujeni", 150),
                ("Constanța, Mamaia", "Mangalia, Centru", 45),
                ("Brașov, Coresi", "Sfântu Gheorghe, Centru", 35),
                ("Galați, Mazepa", "Brăila, Viziru", 40),
                ("Oradea, Nufărul", "Cluj, Zorilor", 140),
                ("Craiova, Craiovița", "Târgu Jiu, Centru", 110),
                ("Alba Iulia, Cetate", "Deva, Micro 15", 80)
            };

            var statuses = new[] { "Completed", "In Progress", "Planned" };

            for (int i = 0; i < 20; i++)
            {
                var location = locations[i];
                var distanceKm = location.Item3;
                var estimatedCost = (decimal)(distanceKm * 1.8 + rand.Next(50, 150)); 
                var revenue = estimatedCost * (decimal)(1.3 + rand.NextDouble() * 0.5);
                var actualCost = i < 10 ? estimatedCost * (decimal)(0.9 + rand.NextDouble() * 0.3) : 0;
                var fuelCost = i < 10 ? (decimal)(distanceKm / 100.0) * vehicles[i % 10].FuelConsumptionPer100Km * 7.5m : 0; 
                
                var status = i < 10 ? "Completed" : (i < 14 ? "In Progress" : "Planned");
                
                var delivery = new Delivery
                {
                    UserId = userId,
                    PickupLocation = location.Item1,
                    DropoffLocation = location.Item2,
                    ScheduledDate = DateTime.UtcNow.AddDays(-rand.Next(1, 60)).AddHours(rand.Next(6, 20)),
                    Status = status,
                    DriverId = drivers[i % 10].Id,
                    VehicleId = vehicles[i % 10].Id,
                    
                    DistanceKm = distanceKm,
                    EstimatedCost = Math.Round(estimatedCost, 2),
                    Revenue = Math.Round(revenue, 2),
                    ActualCost = Math.Round(actualCost, 2),
                    FuelCost = Math.Round(fuelCost, 2),
                    
                    CurrentLatitude = status == "In Progress" ? 44.4268 + rand.NextDouble() * 2 : null,
                    CurrentLongitude = status == "In Progress" ? 26.1025 + rand.NextDouble() * 2 : null,
                    LastLocationUpdate = status == "In Progress" ? DateTime.UtcNow.AddMinutes(-rand.Next(1, 30)) : null,
                    
                    Notes = i % 3 == 0 ? $"Transport prioritar - confirmat client la {DateTime.Now:HH:mm}" : null
                };

                _context.Deliveries.Add(delivery);
            }

            await _context.SaveChangesAsync();
            
            Console.WriteLine($"Seeded {vehicles.Count} vehicles, {drivers.Count} drivers, and 20 deliveries for user {userId}");
        }
    }
}
