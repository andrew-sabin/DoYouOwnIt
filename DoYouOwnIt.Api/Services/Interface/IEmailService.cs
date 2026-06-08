using DoYouOwnIt_Shared.Models.Emails;

namespace DoYouOwnIt.Api.Services.Interface
{
    public interface IEmailService
    {
        Task<EmailResponse> SendEmailAsync(SendEmailRequest request);
        Task SendConfirmation(string? userName, string? code, string? email);
        Task SendResetEmail(string? userName, string? code, string? email);
        Task SendLoginNotification(string userName, string? code, string? email, string? ipAddress);
    }
}
