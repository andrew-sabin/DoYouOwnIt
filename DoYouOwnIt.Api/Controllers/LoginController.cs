using DoYouOwnIt.Shared.Models.Login;
using DoYouOwnIt.Shared.Models.Token;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            if (result.Success && result.RefreshToken != null && result.Token != null)
            {
                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponse>> RefreshTokens()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Unauthorized(new LoginResponse(false)
                {
                    Error = "Refresh token cookie is missing."
                });
            }

            var result = await _loginService.RefreshTokensAsync(refreshToken);
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            else if (string.IsNullOrWhiteSpace(result.Token) || string.IsNullOrWhiteSpace(result.RefreshToken))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new LoginResponse(false)
                {
                    Error = "Token generation failed."
                });
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogoutCookies()
        {
            Response.Cookies.Delete("refreshToken");
            return Ok();
        }
    }
}
