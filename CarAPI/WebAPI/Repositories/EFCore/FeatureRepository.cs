using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repositories.Base;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories.EFCore
{
    public class FeatureRepository : RepositoryBase<Feature>, IFeatureRepository
    {
        public FeatureRepository(RepositoryDbContext context) : base(context)
        {
        }
    }
}
