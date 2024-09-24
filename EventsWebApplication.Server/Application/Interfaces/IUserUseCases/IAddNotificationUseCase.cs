using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IAddNotificationUseCase
    {
        public Task ExecuteAsync(NotificationCreateDto notificationDto);
    }
}
