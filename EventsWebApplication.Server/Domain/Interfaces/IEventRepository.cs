﻿using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface IEventRepository
    {
        // к сожалению слово event зарезервированно, потому приходится вот так выкручиваться
        Task<Event> GetEventByIdAsync(int id);
/*        Task<Event> GetEvetnsByTitleAsync(string title);
        Task<Event> GetEventsByDataAsync(string data);
        Task<Event> GetEventsByPlaceAsync(string place);
        Task<Event> GetEventsByTypeAsync(string type);*/
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<User>> GetUsersByEventIdAsync(int eventId);
        Task RegisterUserToEventAsync(int userId, int eventId);
        Task UnregisterUserFromEventAsync(int userId, int eventId);
        Task AddEventAsync(Event eventObject);
        Task UpdateEventAsync(Event eventOjbect);
        Task DeleteEventAsync(int id);
    }

}
