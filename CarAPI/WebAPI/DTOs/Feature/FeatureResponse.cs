namespace WebAPI.DTOs.Feature
{
    public class FeatureResponse
    {
        public int FeatureId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
