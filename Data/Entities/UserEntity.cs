using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }
        public UserAddressEntity? Address { get; set; }
    }
}
