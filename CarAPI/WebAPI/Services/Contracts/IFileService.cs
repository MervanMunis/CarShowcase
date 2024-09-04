namespace WebAPI.Services.Contracts
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string filePath);
        Task<byte[]> GetImageByCarIdAsync(int carId);
    }
}
