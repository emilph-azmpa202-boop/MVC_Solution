using solution.Services.Interfaces;

namespace solution.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(
            IFormFile file,
            string folder)
        {
            string fileName =
                Guid.NewGuid() +
                Path.GetExtension(file.FileName);

            string path =
                Path.Combine(
                    _env.WebRootPath,
                    folder,
                    fileName);

            using FileStream stream =
                new(path, FileMode.Create);

            await file.CopyToAsync(stream);

            return fileName;
        }

        public void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}