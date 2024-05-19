namespace NZWalks.API.Models.Domain
{
    public class WalkDomain
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation properties
        public DifficultyDomain Difficulty { get; set; }
        public RegionDomain Region { get; set; }
    }
}
