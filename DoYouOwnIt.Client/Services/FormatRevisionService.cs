using DoYouOwnIt.Shared.Models.Format;
using DoYouOwnIt.Shared.Models.FormatRevision;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class FormatRevisionService : IFormatRevisionService
    {
        private readonly HttpClient _httpClient;
        public FormatRevisionService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<FormatRevisionResponse> FormatRevisions { get; set; } = new List<FormatRevisionResponse>();
        public event Action? OnChange;
        public async Task<FormatRevisionResponse> CreateRevision(FormatRevisionRequest request)
        {
            var createRequest = new FormatRevisionCreateRequest
            {
                IsInPrint = request.IsInPrint,
                Description = request.Description,
                OwnershipLevel = request.OwnershipLevel,
                IsAIAssisted = request.IsAIAssisted,
                AIAssistsWith = request.AIAssistsWith,
                FormatId = request.FormatId,
                PreviousRevisionId = request.PreviousRevisionId,
                RevisionNumber = request.RevisionNumber
            };
            var response = await _httpClient.PostAsJsonAsync("api/formatrevision", createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
            var createdRevision = await response.Content.ReadFromJsonAsync<FormatRevisionResponse>();
            return createdRevision;


            throw new NotImplementedException();
        }

        public async Task DeleteRevision(int revisionId)
        {
            await _httpClient.DeleteAsync($"api/formatrevision/{revisionId}");
        }

        public async Task<FormatRevisionResponse?> GetMostRecentRevision(int formatId)
        {
            return await _httpClient.GetFromJsonAsync<FormatRevisionResponse>($"api/formatrevision/format/recent/{formatId}");
        }

        public async Task<FormatRevisionResponse?> GetRevisionById(int revisionId)
        {
            return await _httpClient.GetFromJsonAsync<FormatRevisionResponse>($"api/formatrevision/{revisionId}");
        }

        public async Task<List<FormatRevisionResponse>> GetRevisionsByFormatId(int formatId)
        {
            List<FormatRevisionResponse>? revisions = await _httpClient.GetFromJsonAsync<List<FormatRevisionResponse>>($"api/formatrevision/format/{formatId}");
            if (revisions == null)
                return null!;
            else
                return revisions;
        }
    }
}
