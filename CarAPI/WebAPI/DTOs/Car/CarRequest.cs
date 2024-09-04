using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Car
{
    public class CarRequest
    {
        [Required]
        public int BrandId { get; set; }

        [Required]
        public int ModelId { get; set; }

        public List<int> FeatureIds { get; set; } = [];
    }
}
