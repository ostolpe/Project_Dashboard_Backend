using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ClientEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? ImageUrl { get; set; }

        public ClientAddressEntity? Address { get; set; }
    }
}
