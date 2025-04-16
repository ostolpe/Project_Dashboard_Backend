﻿namespace Business.Dtos
{
    public class Project
    {
        public string Id { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; } = null!;
        public Client Client { get; set; } = null!;
        public Status Status { get; set; } = null!;
    }
}
