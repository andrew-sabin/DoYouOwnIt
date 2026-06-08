using DoYouOwnIt_Shared.Models.Emails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<ActionResult<EmailResponse>> SendEmail([FromBody] SendEmailRequest request)
        {
            // Add validation
            if (string.IsNullOrEmpty(request.ToEmail) || string.IsNullOrEmpty(request.Subject))
            {
                return BadRequest(new EmailResponse
                {
                    Success = false,
                    Message = "Email and subject are required"
                });
            }

            var result = await _emailService.SendEmailAsync(request);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, result);
        }
    }
}
