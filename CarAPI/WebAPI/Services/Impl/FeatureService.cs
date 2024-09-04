using AutoMapper;
using WebAPI.DTOs.Feature;
using WebAPI.Models;
using WebAPI.Repositories.Manager;
using WebAPI.Services.Contracts;

namespace WebAPI.Services.Impl
{
    public class FeatureService : IFeatureService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public FeatureService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<FeatureResponse> GetAllFeaturesAsync()
        {
            var features = _repositoryManager.FeatureRepository.GetAll(trackChanges: false);
            return _mapper.Map<IEnumerable<FeatureResponse>>(features);
        }

        public FeatureResponse GetFeatureByIdAsync(int featureId)
        {
            var feature = _repositoryManager.FeatureRepository.FindByCondition(f => f.FeatureId == featureId, trackChanges: false);
            return _mapper.Map<FeatureResponse>(feature.FirstOrDefault());
        }

        public async Task<string> CreateFeatureAsync(FeatureRequest featureRequest)
        {
            var featureEntity = _mapper.Map<Feature>(featureRequest);
            await _repositoryManager.FeatureRepository.CreateAsync(featureEntity);
            await _repositoryManager.SaveChangesAsync();
            return "The Feature Is Created.";
        }

        public async Task<string> CreateFeatureAsync(IEnumerable<FeatureRequest> featureRequests)
        {
            var featureEntities = _mapper.Map<IEnumerable<Feature>>(featureRequests);
            await _repositoryManager.FeatureRepository.CreateAsync(featureEntities);
            await _repositoryManager.SaveChangesAsync();
            return "The Features Are Created.";
        }

        public async Task UpdateFeatureAsync(int featureId, FeatureRequest featureRequest)
        {
            var featureEntity = _repositoryManager.FeatureRepository.FindByCondition(f => f.FeatureId == featureId, trackChanges: true);
            if (featureEntity == null || !featureEntity.Any())
            {
                throw new Exception("Feature not found");
            }
            _mapper.Map(featureRequest, featureEntity.First());
            _repositoryManager.FeatureRepository.Update(featureEntity.First());
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task DeleteFeatureAsync(int featureId)
        {
            var featureEntity = _repositoryManager.FeatureRepository
                .FindByCondition(f => f.FeatureId == featureId, trackChanges: true);

            if (featureEntity == null || !featureEntity.Any())
            {
                throw new Exception("Feature not found");
            }
            _repositoryManager.FeatureRepository.Delete(featureEntity.First());
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
