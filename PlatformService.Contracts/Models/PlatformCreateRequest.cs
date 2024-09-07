using System.Text.Json.Serialization;

namespace PlatformService.Contracts.Models
{
    public class PlatformCreateRequest
    {
        [JsonIgnore]
        public Ulid Ulid { get; set; } = Ulid.NewUlid();
        public required string Name { get; set; }
        public required string Publisher { get; set; }
        public required string Cost { get; set; }
    }
}
