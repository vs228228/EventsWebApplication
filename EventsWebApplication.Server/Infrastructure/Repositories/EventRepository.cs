using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Server.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddEventAsync(Event eventObject)
        {
            await _context.Events.AddAsync(eventObject);
        }

        public async Task DeleteEventAsync(int id)
        {
            Event entity = await _context.Events.FindAsync(id);
            _context.Events.Remove(entity);
        }

        public async Task UpdateEventAsync(Event eventOjbect)
        {
            _context.Events.Update(eventOjbect);
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersByEventIdAsync(int eventId)
        {
            return await _context.Participants
            .Where(ep => ep.EventId == eventId)
            .Select(ep => ep.User)
            .ToListAsync();
        }

        public async Task<PagedResult<Event>> GetEvensAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Events.CountAsync();
            var events = await _context.Events
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Event>
            {
                TotalCount = totalCount,
                Items = events,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task RegisterUserForEventAsync(int userId, int eventId)
        {
            var participant = new EventParticipant
            {
                UserId = userId,
                EventId = eventId,
                RegistrationDate = DateTime.UtcNow
            };
           await _context.Participants.AddAsync(participant);
        }

        public async Task UnregisterUserFromEventAsync(int userId, int eventId)
        {
            EventParticipant entity = await _context.Participants.FindAsync(userId, eventId);
            _context.Participants.Remove(entity);
        }
    }
}
