using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
