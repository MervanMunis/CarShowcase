using AutoMapper;
using WebAPI.DTOs.Model;
using WebAPI.Models;
using WebAPI.Repositories.Manager;
using WebAPI.Services.Contracts;

namespace WebAPI.Services.Impl
{
    public class ModelService : IModelService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ModelService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<ModelResponse> GetAllModelsAsync()
        {
            var models = _repositoryManager.ModelRepository.GetAll(trackChanges: false);
            return _mapper.Map<IEnumerable<ModelResponse>>(models);
        }

        public ModelResponse GetModelByIdAsync(int modelId)
        {
            var model = _repositoryManager.ModelRepository.FindByCondition(m => m.ModelId == modelId, trackChanges: false);
            return _mapper.Map<ModelResponse>(model.FirstOrDefault());
        }

        public async Task<string> CreateModelAsync(ModelRequest modelRequest)
        {
            var modelEntity = _mapper.Map<Model>(modelRequest);

            // Handle features in the cross-table
            if (modelRequest.FeatureIds != null && modelRequest.FeatureIds.Any())
            {
                modelEntity.ModelFeatures = modelRequest.FeatureIds.Select(featureId => new ModelFeature
                {
                    FeatureId = featureId,
                    ModelId = modelEntity.ModelId
                }).ToList();
            }

            await _repositoryManager.ModelRepository.CreateAsync(modelEntity);
            await _repositoryManager.SaveChangesAsync();

            return "The Model Is Created.";
        }

        public async Task<string> CreateModelAsync(IEnumerable<ModelRequest> modelRequests)
        {
            var modelEntities = _mapper.Map<IEnumerable<Model>>(modelRequests);

            // Handle features for each model in the cross-table
            foreach (var modelEntity in modelEntities)
            {
                var correspondingRequest = modelRequests.First(r => r.Name == modelEntity.Name);

                if (correspondingRequest.FeatureIds != null && correspondingRequest.FeatureIds.Any())
                {
                    modelEntity.ModelFeatures = correspondingRequest.FeatureIds.Select(featureId => new ModelFeature
                    {
                        FeatureId = featureId,
                        ModelId = modelEntity.ModelId
                    }).ToList();
                }
            }

            await _repositoryManager.ModelRepository.CreateAsync(modelEntities);
            await _repositoryManager.SaveChangesAsync();

            return "The Models Are Created.";
        }

        public async Task UpdateModelAsync(int modelId, ModelRequest modelRequest)
        {
            var modelEntity = _repositoryManager.ModelRepository.FindByCondition(m => m.ModelId == modelId, trackChanges: true);
            if (modelEntity == null || !modelEntity.Any())
            {
                throw new Exception("Model not found");
            }

            // Update model details
            _mapper.Map(modelRequest, modelEntity.First());
            _repositoryManager.ModelRepository.Update(modelEntity.First());

            // Handle features in the cross-table
            var existingFeatures = _repositoryManager.ModelFeatureRepository.FindByCondition(mf => mf.ModelId == modelId, trackChanges: true);
            _repositoryManager.ModelFeatureRepository.Delete(existingFeatures);

            if (modelRequest.FeatureIds != null && modelRequest.FeatureIds.Any())
            {
                modelEntity.First().ModelFeatures = modelRequest.FeatureIds.Select(featureId => new ModelFeature
                {
                    FeatureId = featureId,
                    ModelId = modelEntity.First().ModelId
                }).ToList();
            }

            await _repositoryManager.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int modelId)
        {
            var modelEntity = _repositoryManager.ModelRepository.FindByCondition(m => m.ModelId == modelId, trackChanges: true);
            if (modelEntity == null || !modelEntity.Any())
            {
                throw new Exception("Model not found");
            }

            _repositoryManager.ModelRepository.Delete(modelEntity.First());

            // Delete associated features
            var modelFeatures = _repositoryManager.ModelFeatureRepository.FindByCondition(mf => mf.ModelId == modelId, trackChanges: true);
            _repositoryManager.ModelFeatureRepository.Delete(modelFeatures);

            await _repositoryManager.SaveChangesAsync();
        }
    }
}
