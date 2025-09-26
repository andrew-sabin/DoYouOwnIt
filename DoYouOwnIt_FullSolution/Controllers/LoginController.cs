using DoYouOwnIt.Shared.Models.Login;
using DoYouOwnIt.Shared.Models.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var result = await _loginService.LoginAsync(request);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponse>> RefreshTokens(RefreshTokenRequest request)
        {
            var result = await _loginService.RefreshTokensAsync(request);
            if (result is null || result.Value.Token is null || result.Value.RefreshToken is null)
            {
                return Unauthorized(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
