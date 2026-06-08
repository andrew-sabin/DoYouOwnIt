using DoYouOwnIt_Shared.Models.Captcha;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CaptchaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] string request)
        {
            using var httpClient = new HttpClient();
            var secretKey = _configuration["ReCaptcha:Secret"];
            var response = await httpClient.GetStringAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={request}");

            var result = JsonSerializer.Deserialize<ReCaptchaResponse>(response);
            return Ok(new { success = result?.Success ?? false });
        }
    }
}
