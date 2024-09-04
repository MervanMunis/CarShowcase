using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Repositories.Contracts;
using WebAPI.Repositories.EFCore;

namespace WebAPI.Repositories.Manager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryDbContext _context;

        private readonly Lazy<IBrandRepository> _brandRepository;
        private readonly Lazy<ICarFeatureRepository> _carFeatureRepository;
        private readonly Lazy<ICarRepository> _carRepository;
        private readonly Lazy<IFeatureRepository> _featureRepository;
        private readonly Lazy<IModelFeatureRepository> _modelFeatureRepository;
        private readonly Lazy<IModelRepository> _modelRepository;

        public RepositoryManager(RepositoryDbContext repositoryDbContext)
        {
            _context = repositoryDbContext;

            _brandRepository = new Lazy<IBrandRepository>(() => new BrandRepository(_context));
            _carFeatureRepository = new Lazy<ICarFeatureRepository>(() => new CarFeatureRepository(_context));
            _carRepository = new Lazy<ICarRepository>(() => new CarRepository(_context));
            _featureRepository = new Lazy<IFeatureRepository>(() => new FeatureRepository(_context));
            _modelFeatureRepository = new Lazy<IModelFeatureRepository>(() => new ModelFeatureRepository(_context));
            _modelRepository = new Lazy<IModelRepository>(() => new ModelRepository(_context));
        }

        public IBrandRepository BrandRepository => _brandRepository.Value;
        public ICarFeatureRepository CarFeatureRepository => _carFeatureRepository.Value;
        public ICarRepository CarRepository => _carRepository.Value;
        public IFeatureRepository FeatureRepository => _featureRepository.Value;
        public IModelFeatureRepository ModelFeatureRepository => _modelFeatureRepository.Value;
        public IModelRepository ModelRepository => _modelRepository.Value;


        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
