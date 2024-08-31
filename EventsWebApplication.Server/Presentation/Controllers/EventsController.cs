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
        public async Task<IActionResult> GetUsersAsync(int pageNumber, int pageSize)
        {
            var events = _eventService.GetEventsAsync(pageNumber, pageSize);
            return Ok(events);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllEventsAsync()
        {

            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventByIdAsync(int id)
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
        public async Task<IActionResult> CreateEventAsync([FromForm] EventCreateDto eventCreateDto,  IFormFile photo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _eventService.AddEventAsync(eventCreateDto, photo);
            return Created();
        }

        [HttpPost("api/Events/registerForEvent")]
        public async Task<IActionResult> RegisterForEventAsync(int eventId, int userId)
        {
            var info = new UserEventIdDto() { EventId = eventId, UserId = userId };
            await _eventService.RegisterUserForEventAsync(info);
            return Ok();
        }

        [HttpPost("api/Events/unregisterForEvent")]
        public async Task<IActionResult> UnregisterForEventAsync(int eventId, int userId)
        {
            var info = new UserEventIdDto() { EventId = eventId, UserId = userId };
            await _eventService.UnregisterUserFromEventAsync(info);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventAsync([FromForm] EventUpdateDto eventUpdateDto,  IFormFile photo = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _eventService.UpdateEventAsync(eventUpdateDto, photo);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            /*catch
            {
                return BadRequest();
            }*/
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventAsync(int id)
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
