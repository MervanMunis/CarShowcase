using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Foreign Key to Brand
        [Required]
        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; }

        // Navigation Property: One Model has many Features
        public ICollection<ModelFeature>? ModelFeatures { get; set; }
    }
}
