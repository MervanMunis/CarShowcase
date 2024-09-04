using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repositories.Base;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories.EFCore
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryDbContext context) : base(context)
        {
        }

        public IEnumerable<Car> GetAllCarsAsync(bool trackChanges)
        {
            return GetAll(trackChanges)
                .Include(m => m.Model)
                .Include(b => b.Brand)
                .Include(f => f.CarFeatures)!
                    .ThenInclude(cf => cf.Feature)
                .OrderBy(c => c.Model!.Name)
                .ToList();
        }

        public Car GetCarByIdAsync(int carId, bool trackChanges)
        {
            return  FindByCondition(c => c.CarId == carId, trackChanges)
                .Include(m => m.Model)
                .Include(f => f.CarFeatures)
                    .ThenInclude(cf => cf.Feature)
                .Include(b => b.Brand)
                .FirstOrDefault();
        }
    }
}
