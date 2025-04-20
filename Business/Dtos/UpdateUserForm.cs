using System.ComponentModel.DataAnnotations;

namespace Business.Dtos
{
    public class UpdateUserForm
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? StreetName { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public string Role { get; set; } = null!;
    }
}
