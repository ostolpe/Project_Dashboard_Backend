using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos
{
    public class UpdateClientForm
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public IFormFile? NewImageUrl { get; set; }
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
