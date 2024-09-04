using WebAPI.DTOs.Model;

namespace WebAPI.Services.Contracts
{
    public interface IModelService
    {
        IEnumerable<ModelResponse> GetAllModelsAsync();
        ModelResponse GetModelByIdAsync(int modelId);
        Task<string> CreateModelAsync(ModelRequest modelRequest);
        Task<string> CreateModelAsync(IEnumerable<ModelRequest> modelRequest);
        Task UpdateModelAsync(int modelId, ModelRequest modelRequest);
        Task DeleteModelAsync(int modelId);
    }
}
