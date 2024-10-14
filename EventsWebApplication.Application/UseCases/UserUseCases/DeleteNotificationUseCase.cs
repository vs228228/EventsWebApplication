using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class DeleteNotificationUseCase : IDeleteNotificationUseCase
    {
        IUnitOfWork _unitOfWork;

        public DeleteNotificationUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int notificationId)
        {
            var not = await _unitOfWork.Notifications.GetByIdAsync(notificationId);
            if (not == null)
            {
                throw new KeyNotFoundException();
            }
            await _unitOfWork.Notifications.DeleteAsync(notificationId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
