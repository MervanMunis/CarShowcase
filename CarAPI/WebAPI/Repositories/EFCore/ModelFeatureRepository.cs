using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repositories.Base;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories.EFCore
{
    public class ModelFeatureRepository : RepositoryBase<ModelFeature>, IModelFeatureRepository
    {
        public ModelFeatureRepository(RepositoryDbContext context) : base(context)
        {
        }
    }
}
