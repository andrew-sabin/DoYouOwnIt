using DoYouOwnIt_Shared.Models.Emails;

namespace DoYouOwnIt.Client.Services
{
    public interface IEmailService
    {
        Task<EmailResponse> SendEmailAsync(SendEmailRequest request);
        Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, string confirmationLink);
        Task<bool> SendSimpleEmailAsync(string toEmail, string subject, string body);
    }
}
