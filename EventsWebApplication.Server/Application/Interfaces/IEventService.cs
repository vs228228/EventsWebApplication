﻿using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface IEventService
    {
        Task<EventDto> GetEventByIdAsync(int id);
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<IEnumerable<UserDto>> GetUsersByEventIdAsync(int eventId);
        Task RegisterUserForEventAsync(UserEventIdDto userEventInfo);
        Task UnregisterUserFromEventAsync(UserEventIdDto userEventInfo);
        Task AddEventAsync(EventCreateDto eventObject, IFormFile photo);
        Task UpdateEventAsync(EventUpdateDto eventObject, IFormFile photo);
        Task DeleteEventAsync(int id);
    }
}
