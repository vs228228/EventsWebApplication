namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string hashedPassword, string providedPassword);
    }
}
