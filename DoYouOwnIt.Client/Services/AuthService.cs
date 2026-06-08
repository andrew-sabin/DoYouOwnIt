using Blazored.LocalStorage;
using DoYouOwnIt.Shared.Models.Account;
using DoYouOwnIt.Shared.Models.Login;
using DoYouOwnIt.Shared.Models.Token;
using DoYouOwnIt_Shared.Models.Emails;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace DoYouOwnIt.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;
        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService, 
            AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/login", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response.Success && response.Token != null)
                {
                    await _localStorageService.SetItemAsStringAsync("authToken", response.Token);
                    await _localStorageService.SetItemAsync("tokenExpiresAt", response.ExpiresAt);
                    await _authenticationStateProvider.GetAuthenticationStateAsync();
                }
                return response;
            }
            return new LoginResponse(false);
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            await _localStorageService.RemoveItemAsync("tokenExpiresAt");
            await _httpClient.PostAsync("api/login/logout", null);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("login");
        }

        public async Task<LoginResponse> RefreshToken()
        {
            var result = await _httpClient.PostAsync("api/login/refresh", null);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response.Success && response.Token != null)
                {
                    await _localStorageService.SetItemAsStringAsync("authToken", response.Token);
                    await _localStorageService.SetItemAsync("tokenExpiresAt", response.ExpiresAt);
                }
                return response;
            }
            return new LoginResponse(false);
        }

        public async Task<AccountRegistrationResponse> Register(AccountRegistrationRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/register", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<AccountRegistrationResponse>();
                return response;
            }
            return new AccountRegistrationResponse(false);
        }

        public async Task EnsureTokenValidAsync()
        {
            // Read expiry as nullable to detect missing/unset values
            var expiresAt = await _localStorageService.GetItemAsync<DateTime?>("tokenExpiresAt");

            if (!expiresAt.HasValue || expiresAt.Value == default)
            {
                // No expiry stored; nothing to do or force logout
                return;
            }

            // Refresh 1 min before expiry
            if (DateTime.UtcNow >= expiresAt.Value.AddSeconds(-60))
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                if (user != null)
                {
                    var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userIdString == null)
                    {
                        await Logout();
                        return;
                    }

                    if (!Guid.TryParse(userIdString, out var userId))
                    {
                        await Logout();
                        return;
                    }

                    var response = await RefreshToken();
                    if (!response.Success)
                    {
                        await Logout();
                    }
                    // Handle response as needed
                }


            }
        }

        public async Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/forgot-password", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<ForgotPasswordResponse>();
                return response;    
            }
            else
            {
                return new ForgotPasswordResponse
                {
                    Success = false,
                    Message = "Could not connect to server. Please try again later."
                };
            }
        }
    }
}
