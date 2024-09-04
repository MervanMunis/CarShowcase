using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories.Base;
using WebAPI.Repositories.Manager;
using WebAPI.Services.Contracts;

namespace WebAPI.Services.Impl
{
    public class FileService : IFileService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly string _baseDirectory;

        public FileService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "CarAPI");
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is invalid");
            }

            // Create the folder if it doesn't exist
            var folderPath = Path.Combine(_baseDirectory, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Flush();
            }

            return fileName;
        }

        public Task<bool> DeleteFileAsync(string filePath)
        {
            var fullPath = Path.Combine(_baseDirectory, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public async Task<byte[]> GetImageByCarIdAsync(int carId)
        {
            var car = _repositoryManager.CarRepository.GetCarByIdAsync(carId, false);

            if (car == null || string.IsNullOrEmpty(car.ImageFileName))
            {
                throw new FileNotFoundException("Image not found for the specified author.");
            }

            var filePath = Path.Combine(_baseDirectory, "AuthorImages", car.ImageFileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Image not found for the specified author.");
            }

            return await File.ReadAllBytesAsync(filePath);
        }
    }
}
