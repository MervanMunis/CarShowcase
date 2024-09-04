using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repositories.Base;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories.EFCore
{
    public class CarFeatureRepository : RepositoryBase<CarFeature>, ICarFeatureRepository
    {
        public CarFeatureRepository(RepositoryDbContext context) : base(context)
        {
        }
    }
}
