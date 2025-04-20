using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Client
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        public string? ImageUrl { get; set; }
        [Required]
        public string StreetName { get; set; } = null!;
        [Required]
        public string PostalCode { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string BillingReference { get; set; } = null!;
    }
}
