using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Navigation Property: One Brand has many Models
        public ICollection<Model>? Models { get; set; }
    }
}
