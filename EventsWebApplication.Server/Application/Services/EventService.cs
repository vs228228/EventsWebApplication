using AutoMapper;
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
        public readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddEventAsync(EventCreateDto eventObject)
        {
            Event eventEntity = _mapper.Map<Event>(eventObject);
            await _unitOfWork.Events.AddEventAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            try
            {
                Event eventObject = await _unitOfWork.Events.GetEventByIdAsync(id);
                string message = $"Мероприятие {eventObject.Title} было удалено.";
                await NotifyUsersOfChange(eventObject.Id, message);
                var users = await _unitOfWork.Events.GetUsersByEventIdAsync(id);
                foreach (var user in users)
                {
                    await _unitOfWork.Events.UnregisterUserFromEventAsync(user.Id, id);
                }
                await _unitOfWork.Events.DeleteEventAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task UpdateEventAsync(EventUpdateDto eventObject)
        {
            Event eventEntity = _mapper.Map<Event>(eventObject);
            await _unitOfWork.Events.UpdateEventAsync(eventEntity);
            string message = $"Мероприятие {eventObject.Title} было изменено. Просьба проверить страницу мероприятия.";
            await NotifyUsersOfChange(eventObject.Id, message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _unitOfWork.Events.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            Event eventObject = await _unitOfWork.Events.GetEventByIdAsync(id);
            return _mapper.Map<EventDto>(eventObject);
        }

        public async Task<IEnumerable<UserDto>> GetUsersByEventIdAsync(int eventId)
        {
            var users = await _unitOfWork.Events.GetUsersByEventIdAsync(eventId);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task RegisterUserForEventAsync(UserEventIdDto userEventInfo) // сделать
        {
            await _unitOfWork.Events.RegisterUserForEventAsync(userEventInfo.UserId, userEventInfo.EventId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnregisterUserFromEventAsync(UserEventIdDto userEventInfo) // сделать
        {
            await _unitOfWork.Events.UnregisterUserFromEventAsync(userEventInfo.UserId, userEventInfo.EventId);
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
                await _unitOfWork.Notifications.AddNotificationAsync(notification);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
