using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Model
{
    public class ModelRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Model name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int BrandId { get; set; }

        public List<int> FeatureIds { get; set; } = [];
    }
}
