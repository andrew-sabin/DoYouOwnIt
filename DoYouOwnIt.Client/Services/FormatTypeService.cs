using DoYouOwnIt.Shared.Entities;
using DoYouOwnIt.Shared.Models.Format;
using DoYouOwnIt.Shared.Models.FormatType;
using System.Net.Http;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class FormatTypeService : IFormatTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public FormatTypeService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }
        public List<FormatTypeResponse> FormatTypes {  get; set; } = new List<FormatTypeResponse>();
        public event Action? OnChange;

        public async Task CreateFormatType(FormatTypeRequest formatType)
        {
            await _authService.EnsureTokenValidAsync();

            var createRequest = new FormatTypeCreateRequest
            {
                Name = formatType.Name,
                Description = formatType.Description ?? string.Empty,
                CategoryId = formatType.CategoryId,
                ImageUrl = formatType.ImageUrl,
                HasIssue = formatType.HasIssue,
                Issue = formatType.Issue ?? string.Empty,
                IssueURL = formatType.IssueURL ?? string.Empty
            };
            Console.WriteLine($"create request {createRequest.Name}");
            var response = await _httpClient.PostAsJsonAsync("api/FormatType", createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public async Task DeleteFormatType(int formatTypeID)
        {
            await _httpClient.DeleteAsync($"api/formattype/{formatTypeID}");
        }

        public async Task<FormatTypeResponse?> GetFormatTypeByID(int formatTypeId)
        {
            return await _httpClient.GetFromJsonAsync<FormatTypeResponse>($"api/formattype/{formatTypeId}");
        }

        public async Task<List<FormatTypeResponse>> GetFormatTypesByCategoryId(int categoryId)
        {
            var formatTypesByCat = new List<FormatTypeResponse>();

            formatTypesByCat = await _httpClient.GetFromJsonAsync<List<FormatTypeResponse>>(($"api/formattype/category/{categoryId}"));

            if(formatTypesByCat == null)
                return new List<FormatTypeResponse>();

            return formatTypesByCat;
        }

        public async Task UpdateFormatType(int formatTypeID, FormatTypeRequest formatType)
        {
            await _authService.EnsureTokenValidAsync();

            var updateRequest = new FormatTypeUpdateRequest
            {
                Name = formatType.Name,
                Description = formatType.Description ?? string.Empty,
                CategoryId = formatType.CategoryId,
                ImageUrl = formatType.ImageUrl,
                HasIssue = formatType.HasIssue,
                Issue = formatType.Issue ?? string.Empty,
                IssueURL = formatType.IssueURL ?? string.Empty
            };
            var response = await _httpClient.PutAsJsonAsync($"api/formattype/{formatTypeID}", updateRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
