using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Project
    {
        [Required]
        public string Id { get; set; } = null!;
        public string? ImageUrl { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public User User { get; set; } = null!;
        [Required]
        public Client Client { get; set; } = null!;
        [Required]
        public Status Status { get; set; } = null!;
    }
}
