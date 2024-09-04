using WebAPI.Models;
using WebAPI.Repositories.Base;

namespace WebAPI.Repositories.Contracts
{
    public interface ICarRepository : IRepositoryBase<Car>
    {
        IEnumerable<Car> GetAllCarsAsync(bool trackChanges);
        Car GetCarByIdAsync(int carId, bool trackChanges);
    }
}
