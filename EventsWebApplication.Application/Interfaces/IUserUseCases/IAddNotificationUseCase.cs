using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IAddNotificationUseCase
    {
        public Task ExecuteAsync(NotificationCreateDto notificationDto);
    }
}
