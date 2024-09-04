using WebAPI.DTOs.Brand;
using WebAPI.DTOs.Car;

namespace WebAPI.Services.Contracts
{
    public interface ICarService
    {
        IEnumerable<CarResponse> GetAllCarsAsync();
        CarResponse GetCarByIdAsync(int carId);
        Task<string> CreateCarAsync(CarRequest carRequest);
        Task<string> CreateCarAsync(IEnumerable<CarRequest> carRequests);
        Task UpdateCarAsync(int carId, CarRequest carRequest);
        Task UpdateCarImageByIdAsync(int carId, IFormFile imageFile);
        Task DeleteCarAsync(int carId);
    }
}
