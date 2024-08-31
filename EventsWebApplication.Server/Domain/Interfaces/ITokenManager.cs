using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface ITokenManager
    {
        RefreshToken GenerateRefreshToken();
        string GenerateAccessToken(User user);
    }
}
