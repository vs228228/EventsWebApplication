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
        private readonly IFileService _fileService;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileSerivce)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileSerivce;
        }
        public async Task AddEventAsync(EventCreateDto eventObject, IFormFile photo)
        {
            Event eventEntity = _mapper.Map<Event>(eventObject);
            var photoPath = await _fileService.SaveFileAsync(photo);
            eventEntity.ImagePath = photoPath;
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
            catch
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task UpdateEventAsync(EventUpdateDto eventObject, IFormFile photo)
        {
            
            Event oldEvent = await _unitOfWork.Events.GetEventByIdAsync(eventObject.Id);
            if (oldEvent == null)
            {
                throw new KeyNotFoundException();
            }
            if (photo != null)
            {
                var photoPath = await _fileService.SaveFileAsync(photo);
                oldEvent.ImagePath = photoPath;
            }
            _mapper.Map(eventObject, oldEvent);
            await _unitOfWork.Events.UpdateEventAsync(oldEvent);
            string message = $"Мероприятие {oldEvent.Title} было изменено. Просьба проверить страницу мероприятия.";
            await NotifyUsersOfChange(oldEvent.Id, message);
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
