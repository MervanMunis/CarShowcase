using WebAPI.DTOs.Feature;

namespace WebAPI.DTOs.Car
{
    public class CarResponse
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public int ModelId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public List<FeatureResponse>? Features { get; set; }
    }
}
