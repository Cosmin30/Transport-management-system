using Aplicatie_Transporturi.Data;
using Aplicatie_Transporturi.Entities;

namespace Aplicatie_Transporturi.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task SeedDataForUser(int userId)
        {
            if (_context.Vehicles.Any(v => v.UserId == userId)) return;

            var vehicleModels = new[]
            {
        "Dacia Duster", "Renault Megane", "VW Golf", "Ford Focus",
        "Audi A4", "BMW 320i", "Skoda Octavia", "Seat Ibiza",
        "Toyota Corolla", "Peugeot 308"
    };

            var licensePlates = new[]
            {
        "B101ABC", "B102XYZ", "IF99BOSS", "CJ88POP", "TM45HJK",
        "BV21MNT", "PH44LUX", "IS33MEH", "AG77TAX", "GL10JME"
    };

            var vehicles = Enumerable.Range(0, 10).Select(i => new Vehicle
            {
                UserId = userId,
                LicensePlate = licensePlates[i],
                Model = vehicleModels[i],
                Year = 2010 + i
            });

            var driverNames = new[]
            {
        "Ion Popescu", "Maria Georgescu", "Vasile Ionescu", "Elena Stan",
        "Mihai Andrei", "Ana Tudor", "George Enache", "Florin Petrescu",
        "Radu Pavel", "Andreea Nistor"
    };

            var drivers = Enumerable.Range(0, 10).Select(i => new Driver
            {
                UserId = userId,
                Name = driverNames[i],
                LicenseNumber = $"RODRV-{1000 + i}",
                IsAvailable = i % 2 == 0 // unii disponibili, unii nu
            });

            var locations = new[]
            {
        ("București", "Constanța"),
        ("Cluj", "Timișoara"),
        ("Iași", "Brașov"),
        ("Oradea", "Pitești"),
        ("Arad", "Craiova"),
        ("Sibiu", "Suceava"),
        ("Ploiești", "Bacău"),
        ("Galați", "Focșani"),
        ("Bistrița", "Reșița"),
        ("Tulcea", "Zalău")
    };

            var deliveries = Enumerable.Range(0, 10).Select(i => new Delivery
            {
                UserId = userId,
                PickupLocation = locations[i].Item1,
                DropoffLocation = locations[i].Item2,
                ScheduledDate = DateTime.UtcNow.AddDays(i),
                Status = i % 3 == 0 ? "Completed" : (i % 2 == 0 ? "InProgress" : "Planned")
            });

            await _context.Vehicles.AddRangeAsync(vehicles);
            await _context.Drivers.AddRangeAsync(drivers);
            await _context.Deliveries.AddRangeAsync(deliveries);
            await _context.SaveChangesAsync();
        }

    }

}
