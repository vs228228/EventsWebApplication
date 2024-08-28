using EventsWebApplication.Server.Application.Interfaces;

namespace EventsWebApplication.Server.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file.Length <= 0) return "";

            var fileName = file.FileName;
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(rootPath, "wwwroot/uploads", fileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
        }
    }
}
