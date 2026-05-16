using CarAPI.Entities;

namespace CarAPI.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public CarType Type { get; set; }
        public double Velocity { get; set; }
        public double Price { get; set; }
        public string? OwnerName { get; set; }
    }
}
