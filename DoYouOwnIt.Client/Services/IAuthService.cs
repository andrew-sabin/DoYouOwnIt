using DoYouOwnIt.Shared.Models.Account;
using DoYouOwnIt.Shared.Models.Login;

namespace DoYouOwnIt.Client.Services
{
    public interface IAuthService
    {
        Task<AccountRegistrationResponse> Register(AccountRegistrationRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task Logout();
    }
}
