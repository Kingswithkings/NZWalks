﻿namespace NZWalks.Api.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Description { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set;}
        public Guid RegionId { get; set;}

        //Navigation properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
