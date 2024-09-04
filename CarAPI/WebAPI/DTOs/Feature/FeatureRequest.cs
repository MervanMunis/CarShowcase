using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Feature
{
    public class FeatureRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Feature name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
