using System.ComponentModel.DataAnnotations;

namespace PlatformService.Contracts.Entities
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required Guid Guid { get; set; }
        [Required]
        public required string Publisher { get; set; }
        [Required]
        public required string Cost { get; set; }
    }
}
