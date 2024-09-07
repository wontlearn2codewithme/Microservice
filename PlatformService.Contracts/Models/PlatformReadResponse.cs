namespace PlatformService.Contracts.Models
{
    public class PlatformReadResponse
    {
        public required Ulid Ulid { get; set; }
        public required string Name { get; set; }
        public required string Publisher { get; set; }
        public required string Cost { get; set; }
    }
}
