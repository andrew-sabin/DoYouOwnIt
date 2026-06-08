using DoYouOwnIt.Shared.Models.Account;
using DoYouOwnIt.Shared.Models.Login;
using DoYouOwnIt.Shared.Models.Token;
using DoYouOwnIt_Shared.Models.Emails;

namespace DoYouOwnIt.Client.Services
{
    public interface IAuthService
    {
        Task<AccountRegistrationResponse> Register(AccountRegistrationRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request);
        Task Logout();
        Task<LoginResponse> RefreshToken();
        Task EnsureTokenValidAsync();
    }
}
