namespace solution.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string folder);

        void Delete(string path);
    }
}