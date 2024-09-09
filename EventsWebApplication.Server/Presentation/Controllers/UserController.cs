using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventsWebApplication.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userService.GetUsersAsync(pageNumber, pageSize);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getAll")]
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
            return NotFound("User is not exist");
        }

        [HttpGet("getByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User is not exist");
        }

        [Authorize]
        [HttpGet("getRegisteredEvent/{id}")]
        public async Task<IActionResult> GetRegisteredEventAsync(int id)
        {
            var events = await _userService.GetRegisteredEventsAsync(id);
            return Ok(events);
        }

        [Authorize]
        [HttpGet("notification")]
        public async Task<IActionResult> GetNotificationsAsync(int userId, int pageNumber, int pageSize)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            var user = await _userService.GetUserByEmailAsync(userAuthDto.UserEmail);
            GetTokenDto getTokenDto = new GetTokenDto()
            {
                RefreshToken = ans,
                userId = user.Id
            };
            var newAns = await _userService.GenerateAccessToken(getTokenDto);
            return Ok(newAns);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateDto userCreateDto) // без хешера и токена не работает
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string ans = await _userService.TryAddUserAsync(userCreateDto);
            if (ans == "OK") return Ok(); 
            
            return Conflict("Данный email уже зарегистрирован.");
        }

        [Authorize]
        [HttpPost("notification")]
        public async Task<IActionResult> AddNotificationAsync([FromBody] NotificationCreateDto notificationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.AddNotificationAsync(notificationDto);
            return Ok();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] GetTokenDto getTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tokens = await _userService.GenerateAccessToken(getTokenDto);
            if (tokens == null) return Unauthorized();
            return Ok(tokens);
            
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateDto userUpdateDto) // надо исправить там фичи с паролями и нотификацией
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.UpdateUserAsync(userUpdateDto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
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
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
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
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
