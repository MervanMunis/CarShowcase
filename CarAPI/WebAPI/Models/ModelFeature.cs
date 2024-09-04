using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ModelFeature
    {
        public int ModelId { get; set; }

        [ForeignKey(nameof(ModelId))]
        public Model? Model { get; set; }

        public int FeatureId { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public Feature? Feature { get; set; }
    }
}
