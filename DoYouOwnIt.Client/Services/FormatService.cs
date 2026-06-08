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

        public async Task<FormatResponse> CreateFormat(FormatRequest format)
        {
            var createRequest = new FormatCreateRequest
            {
                FormatTypeId = format.FormatTypeId,
                Type = format.Type,
                Edition = format.Edition,
                ReleaseDate = format.ReleaseDate,
                CoverImageUrl = format.CoverImageUrl,
                ProductId = format.ProductId
            };
            var response = await _httpClient.PostAsJsonAsync("api/format", createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
            var createdFormat = await response.Content.ReadFromJsonAsync<FormatResponse>();
            return createdFormat;
        }

        public async Task DeleteFormat(int formatID)
        {
            await _httpClient.DeleteAsync($"api/format/{formatID}");
        }

        public async Task<FormatResponse?> GetFormatByID(int formatId)
        {
            return await _httpClient.GetFromJsonAsync<FormatResponse>($"api/format/{formatId}");
        }

        public async Task<List<FormatResponse>> GetFormatsByProductId(int productId)
        {
            if (productId <= 0)
            {
                var result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>("api/format");
                if (result == null)
                    return new List<FormatResponse>();
                else
                {
                    return result;
                }
            }
            else
            {
                var result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>($"api/format/product/{productId}");
                if (result == null)
                    return new List<FormatResponse>();
                else
                {
                    return result;
                }
            }
        }

        public async Task<List<FormatResponse>> GetRecentFormats(int amount)
        {
            List<FormatResponse>? result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>($"api/format/recent?amount={amount}");
            if (result != null)
                return result;
            else
                return null!;
        }

        public async Task<List<FormatResponse>> GetRecentlyUpdatedFormats(int amount)
        {
            List<FormatResponse>? result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>($"api/format/recent/updated?amount={amount}");
            if (result != null)
                return result;
            else
                return null!;
        }

        public async Task LockFormat(int formatId, FormatRequest format)
        {
            var lockRequest = new FormatLockRequest
            {
                IsLocked = format.IsLocked,
                lockedReason = format.lockedReason
            };
            await _httpClient.PutAsJsonAsync($"api/format/lock/{formatId}", lockRequest);
        }

        public async Task<FormatResponse> UpdateFormat(int formatID, FormatRequest format)
        {
            var updateRequest = new FormatUpdateRequest
            {
                FormatTypeId = format.FormatTypeId,
                Type = format.Type,
                Edition = format.Edition,
                ReleaseDate = format.ReleaseDate,
                CoverImageUrl = format.CoverImageUrl,
                ProductId = format.ProductId,
                formatrevisionid = format.formatrevisionid
            };
            var response = await _httpClient.PutAsJsonAsync($"api/format/{formatID}", updateRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
            var updatedFormat = await response.Content.ReadFromJsonAsync<FormatResponse>();
            return updatedFormat;
        }
    }
}
