using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories.Manager
{
    public interface IRepositoryManager
    {
        IBrandRepository BrandRepository { get; }
        IModelRepository ModelRepository { get; }
        IFeatureRepository FeatureRepository { get; }
        IModelFeatureRepository ModelFeatureRepository { get; }
        ICarRepository CarRepository { get; }
        ICarFeatureRepository CarFeatureRepository { get; }

        Task SaveChangesAsync();
    }
}
