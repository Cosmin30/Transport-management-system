namespace Aplicatie_Transporturi.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
        public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
    }
}
