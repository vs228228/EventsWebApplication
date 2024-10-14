using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventsWebApplication.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAddNotificationUseCase _addNotificationUseCase;
        private readonly IDeleteNotificationUseCase _deleteNotificationUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly IGenerateAccessTokenUseCase _generateAccessTokenUseCase;
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;
        private readonly IGetNotificationsUseCase _getNotificationsUseCase;
        private readonly IGetRegisteredEventsUseCase _getRegisteredEventsUseCase;
        private readonly IGetUserByEmailUseCase _getUserByEmailUseCase;
        private readonly IGetUserByIdUseCase _getUserByIdUseCase;
        private readonly IGetUsersUseCase _getUsersUseCase;
        private readonly ITryAddUserUseCase _tryAddUserUseCase;
        private readonly ITryAuthenticateUseCase _tryAuthenticateUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;

        public UserController(
            IAddNotificationUseCase addNotificationUseCase,
            IDeleteNotificationUseCase deleteNotificationUseCase,
            IDeleteUserUseCase deleteUserUseCase,
            IGenerateAccessTokenUseCase generateAccessTokenUseCase,
            IGetAllUsersUseCase getAllUsersUseCase,
            IGetNotificationsUseCase getNotificationsUseCase,
            IGetRegisteredEventsUseCase getRegisteredEventsUseCase,
            IGetUserByEmailUseCase getUserByEmailUseCase,
            IGetUserByIdUseCase getUserByIdUseCase,
            IGetUsersUseCase getUsersUseCase,
            ITryAddUserUseCase tryAddUserUseCase,
            ITryAuthenticateUseCase tryAuthenticateUseCase,
            IUpdateUserUseCase updateUserUseCase
            )
        {
            _addNotificationUseCase = addNotificationUseCase;
            _deleteNotificationUseCase = deleteNotificationUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _generateAccessTokenUseCase = generateAccessTokenUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
            _getNotificationsUseCase = getNotificationsUseCase;
            _getRegisteredEventsUseCase = getRegisteredEventsUseCase;
            _getUserByEmailUseCase = getUserByEmailUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getUsersUseCase = getUsersUseCase;
            _tryAddUserUseCase = tryAddUserUseCase;
            _tryAuthenticateUseCase = tryAuthenticateUseCase;
            _updateUserUseCase = updateUserUseCase;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _getUsersUseCase.ExecuteAsync(pageNumber, pageSize);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _getAllUsersUseCase.ExecuteAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {

            var user = await _getUserByIdUseCase.ExecuteAsync(id);
            return Ok(user);
        }

        [HttpGet("getByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _getUserByEmailUseCase.ExecuteAsync(email);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("getRegisteredEvent/{id}")]
        public async Task<IActionResult> GetRegisteredEventsAsync(int id)
        {
            var events = await _getRegisteredEventsUseCase.ExecuteAsync(id);
            return Ok(events);
        }

        [Authorize]
        [HttpGet("notification")]
        public async Task<IActionResult> GetNotificationsAsync(int userId, int pageNumber, int pageSize)
        {
            var notifications = await _getNotificationsUseCase.ExecuteAsync(userId, pageNumber, pageSize);
            return Ok(notifications);
        }

        [HttpPost("authByPassword")]
        public async Task<IActionResult> TryAuthenticateByPasswordAsync([FromBody] UserAuthDto userAuthDto) // без хешера и токена не работает
        {
            var ans = await _tryAuthenticateUseCase.ExecuteAsync(userAuthDto);
            var user = await _getUserByEmailUseCase.ExecuteAsync(userAuthDto.UserEmail);
            GetTokenDto getTokenDto = new GetTokenDto()
            {
                RefreshToken = ans,
                userId = user.Id
            };
            var newAns = await _generateAccessTokenUseCase.ExecuteAsync(getTokenDto);
            return Ok(newAns);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateResponseDto userCreateDto) // без хешера и токена не работает
        {
            await _tryAddUserUseCase.ExecuteAsync(userCreateDto);
            return Ok();

        }

        [Authorize]
        [HttpPost("notification")]
        public async Task<IActionResult> AddNotificationAsync([FromBody] NotificationCreateDto notificationDto)
        {
            await _addNotificationUseCase.ExecuteAsync(notificationDto);
            return Ok();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] GetTokenDto getTokenDto)
        {
            var tokens = await _generateAccessTokenUseCase.ExecuteAsync(getTokenDto);
            return Ok(tokens);

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateRequestDto userUpdateDto) // надо исправить там фичи с паролями и нотификацией
        {

            await _updateUserUseCase.ExecuteAsync(userUpdateDto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {

            await _deleteUserUseCase.ExecuteAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("notification")]
        public async Task<IActionResult> DeleteNotificationAsync(int notificationId)
        {
            await _deleteNotificationUseCase.ExecuteAsync(notificationId);
            return NoContent();
        }
    }
}
