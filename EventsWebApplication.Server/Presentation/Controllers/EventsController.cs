using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApplication.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IAddEventUseCase _addEventUseCase;
        private readonly ICheckUserRegisterForEventUseCase _checkUserRegisterForEventUseCase;
        private readonly IDeleteEventUseCase _deleteEventUseCase;
        private readonly IGetAllEventsUseCase _getAllEventsUseCase;
        private readonly IGetEventsUseCase _getEventsUseCase;
        private readonly IGetEventByIdUseCase _getEventByIdUseCase;
        private readonly IGetUsersByEventIdUseCase _getUsersByEventIdUseCase;
        private readonly IRegisterUserForEventUseCase _registerUserForEventUseCase;
        private readonly IUnregisterUserFromEventUseCase _unregisterUserFromEventUseCase;
        private readonly IUpdateEventUseCase _updateEventUseCase;

        public EventsController(
            IAddEventUseCase addEventUseCase,
            ICheckUserRegisterForEventUseCase checkUserRegisterForEventUseCase,
            IDeleteEventUseCase deleteEventUseCase,
            IGetAllEventsUseCase getAllEventsUseCase,
            IGetEventsUseCase getEventsUseCase,
            IGetEventByIdUseCase getEventByIdUseCase,
            IGetUsersByEventIdUseCase getUsersByEventIdUseCase,
            IRegisterUserForEventUseCase registerUserForEventUseCase,
            IUnregisterUserFromEventUseCase unregisterUserFromEventUseCase,
            IUpdateEventUseCase updateEventUseCase
            )
        {
            _addEventUseCase = addEventUseCase;
            _checkUserRegisterForEventUseCase = checkUserRegisterForEventUseCase;
            _deleteEventUseCase = deleteEventUseCase;
            _getAllEventsUseCase = getAllEventsUseCase;
            _getEventsUseCase = getEventsUseCase;
            _getEventByIdUseCase = getEventByIdUseCase;
            _getUsersByEventIdUseCase = getUsersByEventIdUseCase;
            _registerUserForEventUseCase = registerUserForEventUseCase;
            _unregisterUserFromEventUseCase = unregisterUserFromEventUseCase;
            _updateEventUseCase = updateEventUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsAsync(int pageNumber, int pageSize, string searchString = "")
        {
            var events = _getEventsUseCase.ExecuteAsync(pageNumber, pageSize, searchString);
            return Ok(events);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllEventsAsync()
        {

            var events = await _getAllEventsUseCase.ExecuteAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventByIdAsync(int id)
        {
            var eventObject = await _getEventByIdUseCase.ExecuteAsync(id);
            return Ok(eventObject);
        }

        [Authorize]
        [HttpGet("usersByEvent")]
        public async Task<IActionResult> GetUsersByEventIdAsync(int eventId)
        {
            var users = await _getUsersByEventIdUseCase.ExecuteAsync(eventId);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEventAsync([FromForm] EventCreateRequestDto eventCreateDto,  IFormFile photo = null)
        {
            await _addEventUseCase.ExecuteAsync(eventCreateDto, photo);
            return Created();
        }

        [Authorize]
        [HttpPost("registerForEvent")]
        public async Task<IActionResult> RegisterForEventAsync(UserEventIdDto userEventIdDto)
        {
            await _registerUserForEventUseCase.ExecuteAsync(userEventIdDto);
            return Ok();
        }

        [Authorize]
        [HttpPost("unregisterFromEvent")]
        public async Task<IActionResult> UnregisterForEventAsync(UserEventIdDto userEventIdDto)
        {
            await _unregisterUserFromEventUseCase.ExecuteAsync(userEventIdDto);
            return Ok();
        }

        [Authorize]
        [HttpPost("isUserRegisterToEvent")]
        public async Task<IActionResult> IsUserRegisterToEvent(UserEventIdDto userEventId)
        {
            var ans = await _checkUserRegisterForEventUseCase.ExecuteAsync(userEventId);
            return Ok(ans);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateEventAsync([FromForm] EventUpdateRequestDto eventUpdateDto,  IFormFile photo = null)
        {
                await _updateEventUseCase.ExecuteAsync(eventUpdateDto, photo);
                return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
                await _deleteEventUseCase.ExecuteAsync(id);
                return NoContent();
            
        }

        
    }
}
