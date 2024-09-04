using WebAPI.DTOs.Brand;
using WebAPI.DTOs.Model;

namespace WebAPI.Services.Contracts
{
    public interface IBrandService
    {
        IEnumerable<BrandResponse> GetAllBrandsAsync();
        BrandResponse GetBrandByIdAsync(int brandId);
        Task<string> CreateBrandAsync(BrandRequest brandRequest);
        Task<string> CreateBrandAsync(IEnumerable<BrandRequest> brandRequest);
        Task UpdateBrandAsync(int brandId, BrandRequest brandRequest);
        Task DeleteBrandAsync(int brandId);
    }
}
