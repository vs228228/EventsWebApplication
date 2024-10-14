namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IDeleteNotificationUseCase
    {
        public Task ExecuteAsync(int notificationId);
    }
}
