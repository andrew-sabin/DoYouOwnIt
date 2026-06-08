using DoYouOwnIt.Shared.Models.User;
using DoYouOwnIt_Shared.Models.User;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class UserService : IUserService
    {
        public event Action? OnChange;
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        public UserService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
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
                DateOfBirth = userRequest.DateOfBirth,
                Email = userRequest.Email
            };
            await _authService.EnsureTokenValidAsync();
            await _httpClient.PutAsJsonAsync($"api/user/{userName}", updateRequest);
        }

        public Task<bool> IsUserInRole(string userName, string roleName)
        {
            var response = _httpClient.GetFromJsonAsync<bool>($"api/account/role/check?userName={userName}&roleName={roleName}");
            return response;
        }

        public async Task UpdateUserAdminAsync(string userName, UserRequest userAdminUpdateRequest)
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
            await _authService.EnsureTokenValidAsync();
            await _httpClient.PutAsJsonAsync($"api/user/admin/{userName}", updateRequest);
        }

        public Task<UserResponse?> GetUserByIdAsync(string userId)
        {
            var response = _httpClient.GetFromJsonAsync<UserResponse?>($"api/user?userId={userId}");
            return response;
        }


        public async Task<IList<string>> GetUserRole(string userName)
        {
            var response = await _httpClient.GetFromJsonAsync<IList<string>>($"/api/Account/role/get?userName={userName}");
            if (response == null)
                return [];
            return response;
        }

        public async Task UpdateUserRole(string userName, UserRoleAddRequest roleAdd, UserRemoveRoleRequest roleRemove)
        {
            if (!string.IsNullOrEmpty(roleRemove.removeRole))
                await _httpClient.PostAsJsonAsync($"/api/Account/role/remove", roleRemove);

            if(!string.IsNullOrEmpty(roleAdd.addRole))
                await _httpClient.PostAsJsonAsync($"/api/Account/role", roleAdd);
        }
    }
}
