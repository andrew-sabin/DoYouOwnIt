using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> IsUserInRole(string userName, string roleName)
        {
            var response = await _httpClient.GetAsync($"api/account/role/check?userName={userName}&roleName={roleName}");
            if (response == null)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
