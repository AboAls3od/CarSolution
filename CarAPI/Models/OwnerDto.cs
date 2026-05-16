namespace CarAPI.Models
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CarId { get; set; }
    }
}
