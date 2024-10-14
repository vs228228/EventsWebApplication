using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Interfaces
{
    public interface ITokenManager
    {
        RefreshToken GenerateRefreshToken();
        string GenerateAccessToken(User user);
    }
}
