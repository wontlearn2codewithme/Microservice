using System.Text.Json.Serialization;

namespace PlatformService.Contracts.Models
{
    public class PlatformCreateRequest
    {
        [JsonIgnore]
        public Guid Guid { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Publisher { get; set; }
        public required string Cost { get; set; }
    }
}
