using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using DoYouOwnIt_Shared.Models.Emails;
using DoYouOwnIt.Shared.Configuration;

namespace DoYouOwnIt.Api.Services
{
    public class EmailService: IEmailService
    {
        private readonly Smtp2GoSettings _settings;
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;
        
        public EmailService(IOptions<Smtp2GoSettings> settings, ILogger<EmailService> logger, IConfiguration configuration)
        {
            _settings = settings.Value;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendConfirmation(string? userName, string? code, string? email)
        {
            var clientBaseUrl = _configuration["ClientApp:BaseUrl"] ?? "https://localhost:7169";

            var callbackUrl = $"{clientBaseUrl}/confirm-email?user={userName}&c={code}";

            var confirmationEmail = new SendEmailRequest
            {
                ToEmail = email!,
                ToName = userName!,
                Subject = "DoYouOwnIt Account Registration Confirmation",
                Body = $"""
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset="utf-8" />
                        </head>
                        <body>
                            <img src="https://images.gmntgstrg.com/defaultandresourceimages/DoYouOwnIt-02_fit.svg" 
                            style="max-width: 91px; max-height: 45px;">
                            <h2>DoYouOwnIt Confirmation Email Code</h2>
                            <p>Welcome Aboard {userName}!</p>
                            <p>Here is your confirmation email code:</p>
                            <div class="code"><a href={callbackUrl}>{callbackUrl}</a></div>
                            <p>If you didn't request this, you can ignore this email.</p>
                            <p>Thanks,<br>Andy</p>
                        </body>
                        </html>
                        """,
                IsHtml = true
            };
            await SendEmailAsync(confirmationEmail);
        }

        public async Task<EmailResponse> SendEmailAsync(SendEmailRequest request)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));

                message.To.Add(new MailboxAddress(
                    string.IsNullOrEmpty(request.ToName) ? request.ToEmail : request.ToName,
                    request.ToEmail
                ));

                message.Subject = request.Subject;

                var bodyBuilder = new BodyBuilder();
                if (request.IsHtml)
                    bodyBuilder.HtmlBody = request.Body;
                else
                    bodyBuilder.TextBody = request.Body;

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort,
                                         SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_settings.Username, _settings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Email sent successfully to {request.ToEmail}");

                return new EmailResponse
                {
                    Success = true,
                    Message = "Email sent successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {request.ToEmail}");
                return new EmailResponse
                {
                    Success = false,
                    Message = "Failed to send email"
                };

            }
        }
        /* TODO: Make Notifications for Logging In From An IP address */
        public Task SendLoginNotification(string userName, string? code, string? email, string? ipAddress)
        {
            throw new NotImplementedException();
        }

        public async Task SendResetEmail(string? userName, string? code, string? email)
        {
            var clientBaseUrl = _configuration["ClientApp:BaseUrl"] ?? "https://localhost:7169";

            var callbackUrl = $"{clientBaseUrl}/reset-email?user={userName}&c={code}";

            var resetEmail = new SendEmailRequest
            {
                ToEmail = email!,
                ToName = userName!,
                Subject = "DoYouOwnIt Password Reset Confirmation",
                Body = $"""
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8" />
                </head>
                <body>
                    <img src="https://images.gmntgstrg.com/defaultandresourceimages/DoYouOwnIt-02_fit.svg" 
                    style="max-width: 91px; max-height: 45px;">
                    <h2>Password Reset Code</h2>
                    <p>Hey there {userName}!</p>
                    <p>Use the code below to reset your password:</p>
                    <div class="code"><a href={callbackUrl}>{callbackUrl}</a></div>
                    <p>If you didn't request this, you can ignore this email.</p>
                    <p>Thanks,<br>Andy</p>
                </body>
                </html>
                """,
                IsHtml = true
            };

            await SendEmailAsync(resetEmail);
        }
    }
}
