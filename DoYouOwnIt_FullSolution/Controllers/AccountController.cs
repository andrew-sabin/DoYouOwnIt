using DoYouOwnIt.Shared.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegistrationRequest request)
        {
            var response = await _accountService.RegisterAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [Authorize (Roles = "Admin")]
        [HttpPost("role")]
        public async Task<IActionResult> AssignRole(string userName, string roleName)
        {
            await _accountService.AssignRole(userName, roleName);
            return Ok();
        }
        [Authorize (Roles = "Admin")]
        [HttpPost("role/remove")]
        public async Task<IActionResult> RemoveRole(string userName, string roleName)
        {
            await _accountService.RemoveRole(userName, roleName);
            return Ok();
        }
        [Authorize]
        [HttpGet("role/check")]
        public async Task<IActionResult> IsUserInRole(string userName, string roleName)
        {
            var isInRole = await _accountService.IsUserInRole(userName, roleName);
            return Ok(isInRole);
        }
    }
}
