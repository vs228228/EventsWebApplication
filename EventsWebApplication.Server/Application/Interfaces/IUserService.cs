using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses;
using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<UserResponseDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<EventResponseDto>> GetRegisteredEventsAsync(int userId);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<PagedResult<UserResponseDto>> GetUsersAsync(int pageNumber, int pageSize);
        Task<string> TryAuthenticateAsync(UserAuthDto loginDto);
        Task<string> TryAddUserAsync(UserCreateResponseDto user);
        Task UpdateUserAsync(UserUpdateRequestDto user);
        Task DeleteUserAsync(int id);

        Task<PagedResult<NotificationDto>> GetNotificationsAsync(int userId, int pageNumber, int pageSize);
        Task AddNotificationAsync(NotificationCreateDto notificationDto);
        Task DeleteNotificationAsync(int notificationId);
        Task<TokenDto> GenerateAccessToken(GetTokenDto getTokenDto);
    }
}
