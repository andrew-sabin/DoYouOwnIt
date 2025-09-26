using Blazored.LocalStorage;
using DoYouOwnIt.Shared.Models.Account;
using DoYouOwnIt.Shared.Models.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
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
                    await _authenticationStateProvider.GetAuthenticationStateAsync();
                }
                return response;
            }
            return new LoginResponse(false);
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("login");
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
    }
}
