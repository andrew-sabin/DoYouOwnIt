using DoYouOwnIt.Client.Pages.Products;
using DoYouOwnIt.Shared.Models.Format;
using DoYouOwnIt.Shared.Models.Product;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class FormatService : IFormatService
    {
        private readonly HttpClient _httpClient;

        public FormatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<FormatResponse> Formats { get; set; } = new List<FormatResponse>();
        public event Action? OnChange;

        public async Task CreateFormat(FormatRequest format)
        {
            var createRequest = new FormatCreateRequest
            {
                Type = format.Type,
                Edition = format.Edition,
                ReleaseDate = format.ReleaseDate,
                CoverImageUrl = format.CoverImageUrl,
                Description = format.Description,
                OwnershipLevel = format.OwnershipLevel,
                IsAIAssisted = format.IsAIAssisted,
                AIAssistsWith = format.AIAssistsWith,
                ProductId = format.ProductId
            };
            Console.WriteLine(createRequest);
            await _httpClient.PostAsJsonAsync("api/format", createRequest);
        }

        public async Task DeleteFormat(int formatID)
        {
            await _httpClient.DeleteAsync($"api/format/{formatID}");
        }

        public async Task<FormatResponse?> GetFormatByID(int formatId)
        {
            return await _httpClient.GetFromJsonAsync<FormatResponse>($"api/format/{formatId}");
        }

        public async Task GetFormatsByProductId(int productId)
        {
            List<FormatResponse>? result = null;
            if (productId <= 0)
            {
                result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>("api/format");
            }
            else
            {
                result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>($"api/format/product/{productId}");
            }

            if (result is not null)
            {
                Formats = result;
                OnChange?.Invoke();
            }
            else
            {
                Formats = new List<FormatResponse>();
            }
        }

        public async Task UpdateFormat(int formatID, FormatRequest format)
        {
            var updateRequest = new FormatUpdateRequest
            {
                Type = format.Type,
                Edition = format.Edition,
                ReleaseDate = format.ReleaseDate,
                CoverImageUrl = format.CoverImageUrl,
                Description = format.Description,
                OwnershipLevel = format.OwnershipLevel,
                IsAIAssisted = format.IsAIAssisted,
                AIAssistsWith = format.AIAssistsWith,
                ProductId = format.ProductId
            };
            Console.WriteLine($"Updating product ID: {formatID}");
            await _httpClient.PutAsJsonAsync($"api/format/{formatID}", updateRequest);
        }
    }
}
