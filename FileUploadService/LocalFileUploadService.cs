using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Gaines_Opus_Institute_Current.FileUploadService
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment environment;
        public LocalFileUploadService(IWebHostEnvironment envioronment)
        {
            this.environment = environment;
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(environment.ContentRootPath, "/wwwroot/css/images", file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath;
        }
    }
}
