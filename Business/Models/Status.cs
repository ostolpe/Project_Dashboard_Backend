using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
