using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventsWebApplication.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return  Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("User is not exist");
        }

        [HttpGet("getByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("User is not exist");
        }

        [HttpGet("getRegisteredEvent/{id}")]
        public async Task<IActionResult> GetRegisteredEventAsync(int id)
        {
            var events = await _userService.GetRegisteredEventsAsync(id);
            return Ok(events);
        }

        [HttpGet("notification")]
        public async Task<IActionResult> GetNotificationsAsync(int userId, int pageNumber, int pageSize)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var notifications = await _userService.GetNotificationsAsync(userId, pageNumber, pageSize);
            return Ok(notifications);
        }

        [HttpPost("authByPassword")]
        public async Task<IActionResult> TryAuthenticateByPasswordAsync([FromBody] UserAuthDto userAuthDto) // без хешера и токена не работает
        {
            var ans = await _userService.TryAuthenticateAsync(userAuthDto);
            if(ans == "Wrong email")
            {
                return BadRequest("Wrong email");
            }
            else if (ans == "Wrong password")
            {
                return BadRequest("Wrong password");
            }
            return Ok(ans);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserCreateDto userCreateDto) // без хешера и токена не работает
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            string ans = await _userService.TryAddUserAsync(userCreateDto);
            if (ans == "OK") return Ok();
            return Conflict("Данный email уже зарегистрирован.");
        }

        [HttpPost("notification")]
        public async Task<IActionResult> AddNotificationAsync(NotificationDto notificationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _userService.AddNotificationAsync(notificationDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(UserUpdateDto userUpdateDto) // надо исправить там фичи с паролями и нотификацией
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(userUpdateDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("notification")]
        public async Task<IActionResult> DeleteNotificationAsync(int notificationId)
        {
            try
            {
                await _userService.DeleteNotificationAsync(notificationId);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
