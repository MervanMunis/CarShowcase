using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CarFeature
    {
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car? Car { get; set; }

        public int FeatureId { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public Feature? Feature { get; set; }
    }
}
