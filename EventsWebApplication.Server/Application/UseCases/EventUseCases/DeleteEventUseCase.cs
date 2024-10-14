using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class DeleteEventUseCase : IDeleteEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id)
        {
            
                Event eventObject = await _unitOfWork.Events.GetByIdAsync(id);
                if(eventObject == null) throw new KeyNotFoundException();
                string message = $"Мероприятие {eventObject.Title} было удалено.";
                await NotifyUsersOfChange(eventObject.Id, message);
                var users = await _unitOfWork.Events.GetUsersByEventIdAsync(id);
                foreach (var user in users)
                {
                    await _unitOfWork.Events.UnregisterUserFromEventAsync(user.Id, id);
                }
                await _unitOfWork.Events.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            
        }

        private async Task NotifyUsersOfChange(int eventId, string message)
        {
            var users = await _unitOfWork.Events.GetUsersByEventIdAsync(eventId);
            Notification notification = new Notification()
            {
                Message = message,
                CreatedAt = DateTime.Now
            };
            foreach (var user in users)
            {
                notification.User = user;
                notification.UserId = user.Id;
                await _unitOfWork.Notifications.AddAsync(notification);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
