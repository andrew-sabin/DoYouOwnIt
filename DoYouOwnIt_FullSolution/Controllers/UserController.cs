using DoYouOwnIt.Api.Services.Interface;
using DoYouOwnIt.Shared.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult>? GetUserById(string userId)
        {
            var result = await _userService.GetUserById(userId);
            if (result == null)
            {
                return NotFound($"User with this ID {userId} was not found");
            }
            else
            {
                return Ok(result);
            }

        }

        [AllowAnonymous]
        [HttpGet("{userName}")]
        public async Task<IActionResult>? GetUser(string userName)
        {
            var result = await _userService.GetAsync(userName);
            if (result == null)
            {
                return NotFound($"User with this username {userName} was not found");
            }
            else
            {
                return Ok(result);
            }
                
        }
        [Authorize]
        [HttpPut("{userName}")]
        public async Task<IActionResult>? UpdateUser(string userName, UserUpdateRequest userRequest)
        {
            var currentUserName = User?.Identity?.Name;
            if (userName != currentUserName)
            {
                return StatusCode(403, "Username in the URL does not match the username in the request body.");
            }
            var result = await _userService.UpdateUser(userName, userRequest);
            if (result == null)
            {
                return NotFound($"User with this username {userName} was not found");
            }
            else
            {
                return Ok(result);
            }
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("admin/{userName}")]
        public async Task<IActionResult>? AdminUpdateUser(string userName, AdminUserUpdateRequest userRequest)
        {
            var result = await _userService.AdminUpdateUser(userName, userRequest);
            if (result == null)
            {
                return NotFound($"User with this username {userName} was not found");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
