using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using System.Linq;

namespace EventsWebApplication.Server.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddEventAsync(Event eventObject)
        {
            await _unitOfWork.Events.AddEventAsync(eventObject);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            Event eventObject = await _unitOfWork.Events.GetEventByIdAsync(id);
            string message = $"Мероприятие {eventObject.Title} было удалено.@";
            await NotifyUsersOfChange(eventObject.Id, message);
            var users = await _unitOfWork.Events.GetUsersByEventIdAsync(id);
            foreach (var user in users)
            {
                await _unitOfWork.Events.UnregisterUserFromEventAsync(user.Id, id);
            }
            await _unitOfWork.Events.DeleteEventAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event eventObject)
        {
            await _unitOfWork.Events.UpdateEventAsync(eventObject);
            string message = $"Мероприятие {eventObject.Title} было изменено. Просьба проверить страницу мероприятия.@";
            await NotifyUsersOfChange(eventObject.Id, message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _unitOfWork.Events.GetAllEventsAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await GetEventByIdAsync(id);
        }

        public async Task RegisterUserToEventAsync(UserEventIdDto userEventInfo)
        {
            await _unitOfWork.Events.RegisterUserToEventAsync(userEventInfo.UserId, userEventInfo.EventId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnregisterUserFromEventAsync(UserEventIdDto userEventInfo)
        {
            await _unitOfWork.Events.UnregisterUserFromEventAsync(userEventInfo.UserId, userEventInfo.EventId);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task NotifyUsersOfChange(int eventId, string message)
        {
            var users = await _unitOfWork.Events.GetUsersByEventIdAsync(eventId);
            foreach (var user in users)
            {
                user.NotificationString += message;
                await _unitOfWork.Users.UpdateUserAsync(user);
            }
            await _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
