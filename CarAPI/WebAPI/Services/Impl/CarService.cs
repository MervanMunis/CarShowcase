using AutoMapper;
using WebAPI.DTOs.Car;
using WebAPI.Models;
using WebAPI.Repositories.Manager;
using WebAPI.Services.Contracts;

namespace WebAPI.Services.Impl
{
    public class CarService : ICarService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CarService(IRepositoryManager repositoryManager, IFileService fileService,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _fileService = fileService;
            _mapper = mapper;
        }

        public IEnumerable<CarResponse> GetAllCarsAsync()
        {
            var cars = _repositoryManager.CarRepository.GetAllCarsAsync(trackChanges: false);
            return _mapper.Map<IEnumerable<CarResponse>>(cars);
        }

        public CarResponse GetCarByIdAsync(int carId)
        {
            var car = _repositoryManager.CarRepository.GetCarByIdAsync(carId, trackChanges: false);
            return _mapper.Map<CarResponse>(car);
        }

        public async Task<string> CreateCarAsync(CarRequest carRequest)
        {
            var carEntity = _mapper.Map<Car>(carRequest);

            // Handle features in the cross-table
            if (carRequest.FeatureIds != null && carRequest.FeatureIds.Any())
            {
                carEntity.CarFeatures = carRequest.FeatureIds.Select(featureId => new CarFeature
                {
                    FeatureId = featureId,
                    CarId = carEntity.CarId
                }).ToList();
            }

            await _repositoryManager.CarRepository.CreateAsync(carEntity);
            await _repositoryManager.SaveChangesAsync();

            return "The Car Is Created.";
        }

        public async Task<string> CreateCarAsync(IEnumerable<CarRequest> carRequests)
        {
            var carEntities = _mapper.Map<IEnumerable<Car>>(carRequests);

            // Handle features for each car in the cross-table
            foreach (var carEntity in carEntities)
            {
                var correspondingRequest = carRequests.First(r => r.ModelId == carEntity.ModelId && r.BrandId == carEntity.BrandId);

                if (correspondingRequest.FeatureIds != null && correspondingRequest.FeatureIds.Any())
                {
                    carEntity.CarFeatures = correspondingRequest.FeatureIds.Select(featureId => new CarFeature
                    {
                        FeatureId = featureId,
                        CarId = carEntity.CarId
                    }).ToList();
                }
            }

            await _repositoryManager.CarRepository.CreateAsync(carEntities);
            await _repositoryManager.SaveChangesAsync();

            return "The Cars Are Created.";
        }

        public async Task UpdateCarAsync(int carId, CarRequest carRequest)
        {
            var carEntity = _repositoryManager.CarRepository.FindByCondition(c => c.CarId == carId, trackChanges: true);
            if (carEntity == null || !carEntity.Any())
            {
                throw new Exception("Car not found");
            }

            // Update car details
            _mapper.Map(carRequest, carEntity.First());
            _repositoryManager.CarRepository.Update(carEntity.First());

            // Handle features in the cross-table
            var existingFeatures = _repositoryManager.CarFeatureRepository.FindByCondition(cf => cf.CarId == carId, trackChanges: true);
            _repositoryManager.CarFeatureRepository.Delete(existingFeatures);

            if (carRequest.FeatureIds != null && carRequest.FeatureIds.Any())
            {
                carEntity.First().CarFeatures = carRequest.FeatureIds.Select(featureId => new CarFeature
                {
                    FeatureId = featureId,
                    CarId = carEntity.First().CarId
                }).ToList();
            }

            await _repositoryManager.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int carId)
        {
            var carEntity = _repositoryManager.CarRepository.FindByCondition(c => c.CarId == carId, trackChanges: true);
            if (carEntity == null || !carEntity.Any())
            {
                throw new Exception("Car not found");
            }

            _repositoryManager.CarRepository.Delete(carEntity.First());

            // Delete associated features
            var carFeatures = _repositoryManager.CarFeatureRepository.FindByCondition(cf => cf.CarId == carId, trackChanges: true);
            _repositoryManager.CarFeatureRepository.Delete(carFeatures);

            await _repositoryManager.SaveChangesAsync();
        }

        public async Task UpdateCarImageByIdAsync(int carId, IFormFile imageFile)
        {
            // Find the car by ID
            var car = _repositoryManager.CarRepository.GetCarByIdAsync(carId, true);
            if (car == null)
            {
                throw new ArgumentException("Car not found");
            }

            // If an image already exists, delete it
            if (!string.IsNullOrEmpty(car.ImageFileName))
            {
                await _fileService.DeleteFileAsync(Path.Combine("CarImages", car.ImageFileName));
            }

            // Save the new image
            var newFileName = await _fileService.SaveFileAsync(imageFile, "CarImages");

            // Update the car entity with the new image file name
            car.ImageFileName = newFileName;

            // Save the changes
            _repositoryManager.CarRepository.Update(car);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
