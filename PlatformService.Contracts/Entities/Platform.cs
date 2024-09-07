using System.ComponentModel.DataAnnotations;

namespace PlatformService.Contracts.Entities
{
    public class Platform
    {
        [Key]
        [Required]
        public required int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required Ulid Ulid { get; set; }
        [Required]
        public required string Publisher { get; set; }
        [Required]
        public required string Cost { get; set; }
    }
}
