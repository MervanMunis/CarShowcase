namespace WebAPI.DTOs.Brand
{
    public class BrandResponse
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
