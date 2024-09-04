using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repositories.Base;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories.EFCore
{
    public class ModelRepository : RepositoryBase<Model>, IModelRepository
    {
        public ModelRepository(RepositoryDbContext context) : base(context)
        {
        }
    }
}
