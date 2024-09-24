using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
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
            if(not == null)
            {
                throw new KeyNotFoundException();
            }
            await _unitOfWork.Notifications.DeleteAsync(notificationId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
