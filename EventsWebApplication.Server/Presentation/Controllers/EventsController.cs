using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApplication.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {

            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventObject = await _eventService.GetEventByIdAsync(id);
            if(eventObject == null)
            {
                return NotFound();
            }
            return Ok(eventObject);
        }

        [HttpGet("api/Events/usersByEvent")]
        public async Task<IActionResult> GetUsersByEventIdAsync(int eventId)
        {
            var users = await _eventService.GetUsersByEventIdAsync(eventId);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto eventCreateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _eventService.AddEventAsync(eventCreateDto);
            return Created();
        }
        [HttpPost("api/Events/registerForEvent")]
        public async Task<IActionResult> RegisterForEvent(int eventId, int userId)
        {
            var info = new UserEventIdDto() { EventId = eventId, UserId = userId };
            await _eventService.RegisterUserForEventAsync(info);
            return Ok();
        }

        [HttpPost("api/Events/unregisterForEvent")]
        public async Task<IActionResult> UnregisterForEvent(int eventId, int userId)
        {
            var info = new UserEventIdDto() { EventId = eventId, UserId = userId };
            await _eventService.UnregisterUserFromEventAsync(info);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventUpdateDto eventUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _eventService.UpdateEventAsync(eventUpdateDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                await _eventService.DeleteEventAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        
    }
}
