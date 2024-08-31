using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<EventDto>> GetRegisteredEventsAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber, int pageSize);
        Task<string> TryAuthenticateAsync(UserAuthDto loginDto);
        Task<string> TryAddUserAsync(UserCreateDto user);
        Task UpdateUserAsync(UserUpdateDto user);
        Task DeleteUserAsync(int id);

        Task<PagedResult<NotificationDto>> GetNotificationsAsync(int userId, int pageNumber, int pageSize);
        Task AddNotificationAsync(NotificationDto notificationDto);
        Task DeleteNotificationAsync(int notificationId);
        Task<TokenDto> GenerateAccessToken(GetTokenDto getTokenDto);
    }
}
