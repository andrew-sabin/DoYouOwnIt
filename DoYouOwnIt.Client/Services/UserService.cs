using DoYouOwnIt.Shared.Models.User;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class UserService : IUserService
    {
        public event Action? OnChange;
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponse?> GetUserAsync(string userName)
        {
            var response = await _httpClient.GetFromJsonAsync<UserResponse>($"api/user/{userName}");
            return response;
        }

        public async Task UpdateUserAsync(string userName, UserRequest userRequest)
        {
            var updateRequest = new UserUpdateRequest
            {
                DisplayName = userRequest.DisplayName,
                ProfileImageURL = userRequest.ProfileImageURL,
                WebsiteURL = userRequest.WebsiteURL,
                Bio = userRequest.Bio,
                Email = userRequest.Email
            };
            await _httpClient.PutAsJsonAsync($"api/user/{userName}", updateRequest);
        }

        public Task<bool> IsUserInRole(string userName, string roleName)
        {
            var response = _httpClient.GetFromJsonAsync<bool>($"api/account/role/check?userName={userName}&roleName={roleName}");
            return response;
        }

        public Task UpdateUserAdminAsync(string userName, UserRequest userAdminUpdateRequest)
        {
            var updateRequest = new AdminUserUpdateRequest
            {
                DisplayName = userAdminUpdateRequest.DisplayName,
                UserName = userAdminUpdateRequest.UserName,
                ProfileImageURL = userAdminUpdateRequest.ProfileImageURL,
                Bio = userAdminUpdateRequest.Bio,
                WebsiteURL = userAdminUpdateRequest.WebsiteURL,
                IsVerified = userAdminUpdateRequest.IsVerified,
                IsBanned = userAdminUpdateRequest.IsBanned,
                BanEndDate = userAdminUpdateRequest.BanEndDate,
                BanReason = userAdminUpdateRequest.BanReason
            };
            return _httpClient.PutAsJsonAsync($"api/user/admin/{userName}", updateRequest);
        }

        public Task<UserResponse?> GetUserByIdAsync(string userId)
        {
            var response = _httpClient.GetFromJsonAsync<UserResponse?>($"api/user?userId={userId}");
            return response;
        }
    }
}
