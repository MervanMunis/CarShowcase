using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ImageFileName { get; set; }

        // Foreign Key to Brand
        [Required]
        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; }

        // Foreign Key to Model
        [Required]
        public int ModelId { get; set; }

        [ForeignKey(nameof(ModelId))]
        public Model? Model { get; set; }

        // Navigation Property: One Car has many Features through CarFeature
        public ICollection<CarFeature>? CarFeatures { get; set; }
    }
}
