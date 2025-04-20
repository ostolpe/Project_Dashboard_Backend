using System.ComponentModel.DataAnnotations;

namespace Business.Dtos
{
    public class UpdateProjectForm
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
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public string ClientId { get; set; } = null!;
        public int StatusId { get; set; }
    }
}
