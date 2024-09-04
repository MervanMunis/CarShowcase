using AutoMapper;
using WebAPI.DTOs.Brand;
using WebAPI.Models;
using WebAPI.Repositories.Manager;
using WebAPI.Services.Contracts;

namespace WebAPI.Services.Impl
{
    public class BrandService : IBrandService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public BrandService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<BrandResponse> GetAllBrandsAsync()
        {
            var brands = _repositoryManager.BrandRepository.GetAll(trackChanges: false);
            return _mapper.Map<IEnumerable<BrandResponse>>(brands);
        }

        public BrandResponse GetBrandByIdAsync(int brandId)
        {
            var brand = _repositoryManager.BrandRepository.FindByCondition(b => b.BrandId == brandId, trackChanges: false);
            return _mapper.Map<BrandResponse>(brand.FirstOrDefault());
        }

        public async Task<string> CreateBrandAsync(BrandRequest brandRequest)
        {
            var brandEntity = _mapper.Map<Brand>(brandRequest);
            await _repositoryManager.BrandRepository.CreateAsync(brandEntity);
            await _repositoryManager.SaveChangesAsync();
            return "The Brand Is Created.";
        }

        public async Task<string> CreateBrandAsync(IEnumerable<BrandRequest> brandRequests)
        {
            var brandEntities = _mapper.Map<IEnumerable<Brand>>(brandRequests);

            await _repositoryManager.BrandRepository.CreateAsync(brandEntities);
            await _repositoryManager.SaveChangesAsync();

            return "The Brands Are Created.";
        }

        public async Task UpdateBrandAsync(int brandId, BrandRequest brandRequest)
        {
            var brandEntity = _repositoryManager.BrandRepository.FindByCondition(b => b.BrandId == brandId, trackChanges: true);
            if (brandEntity == null || !brandEntity.Any())
            {
                throw new Exception("Brand not found");
            }
            _mapper.Map(brandRequest, brandEntity.First());
            _repositoryManager.BrandRepository.Update(brandEntity.First());
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task DeleteBrandAsync(int brandId)
        {
            var brandEntity = _repositoryManager.BrandRepository.FindByCondition(b => b.BrandId == brandId, trackChanges: true);
            if (brandEntity == null || !brandEntity.Any())
            {
                throw new Exception("Brand not found");
            }
            _repositoryManager.BrandRepository.Delete(brandEntity.First());
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
