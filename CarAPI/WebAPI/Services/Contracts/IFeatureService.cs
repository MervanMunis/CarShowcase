using WebAPI.DTOs.Car;
using WebAPI.DTOs.Feature;

namespace WebAPI.Services.Contracts
{
    public interface IFeatureService
    {
        IEnumerable<FeatureResponse> GetAllFeaturesAsync();
        FeatureResponse GetFeatureByIdAsync(int featureId);
        Task<string> CreateFeatureAsync(FeatureRequest featureRequest);
        Task<string> CreateFeatureAsync(IEnumerable<FeatureRequest> featureRequests);
        Task UpdateFeatureAsync(int featureId, FeatureRequest featureRequest);
        Task DeleteFeatureAsync(int featureId);
    }
}
