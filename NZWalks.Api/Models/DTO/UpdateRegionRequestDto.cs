namespace NZWalks.Api.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        public Guid Id { get; set; } 
        public string Code { get; set; }
        public string Name { get; set; }
        public string RegionImageUrl { get; set; }
    }
}
