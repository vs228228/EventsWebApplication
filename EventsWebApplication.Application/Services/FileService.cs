using EventsWebApplication.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length <= 0) return "/uploads/default.jpg";

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
