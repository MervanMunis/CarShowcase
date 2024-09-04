using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty; 

        // Navigation Property: One Feature can be associated with many ModelFeatures
        public ICollection<ModelFeature>? ModelFeatures { get; set; }
    }
}
