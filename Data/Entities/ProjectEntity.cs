﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class ProjectEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ImageUrl { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Budget { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public UserEntity User { get; set; } = null!;

        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; } = null!;
        public ClientEntity Client { get; set; } = null!;

        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        public StatusEntity Status { get; set; } = null!;
    }
}
