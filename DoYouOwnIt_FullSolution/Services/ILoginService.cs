using DoYouOwnIt.Shared.Models.Login;
using DoYouOwnIt.Shared.Models.Token;

namespace DoYouOwnIt.Api.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse?> RefreshTokensAsync(RefreshTokenRequest request);
    }
}
