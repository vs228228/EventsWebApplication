using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface ITokenManager
    {
        RefreshToken GenerateRefreshToken();
        string GenerateAccessToken(User user);
    }
}
