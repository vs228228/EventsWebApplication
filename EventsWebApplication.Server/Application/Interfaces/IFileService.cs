namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
