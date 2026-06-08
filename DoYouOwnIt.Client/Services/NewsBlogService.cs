using DoYouOwnIt_Shared.Models.NewsBlog;
using DoYouOwnIt_Shared.Models.NewsUpdate;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class NewsBlogService : INewsBlogService
    {
        private readonly HttpClient _httpClient;

        public NewsBlogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<NewsBlogResponse> NewsBlogs { get; set; } = new List<NewsBlogResponse>();

        public event Action? OnChange;

        public async Task CreateArticle(NewsBlogRequest request)
        {
            var createRequest = new NewsBlogCreateRequest
            {
                Title = request.Title,
                CoverImageUrl = request.CoverImageUrl,
                StickToFrontPage = request.StickToFrontPage,
                ArticleType = request.ArticleType,
                NewsArticle = request.NewsArticle
            };

            var response = await _httpClient.PostAsJsonAsync("api/newsblog",createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public async Task DeleteArticle(int id)
        {
            await _httpClient.DeleteAsync($"api/newsblog/{id}");
        }

        public async Task<NewsBlogResponse?> GetArticleBySlug(string slug)
        {
            return await _httpClient.GetFromJsonAsync<NewsBlogResponse>($"api/newsblog/slug/{slug}");
        }

        public async Task<NewsBlogResponse?> GetFirstTypeArticle(string type)
        {
            return await _httpClient.GetFromJsonAsync<NewsBlogResponse>($"api/newsblog/type/{type}");
        }

        public async Task<List<NewsBlogResponse>> GetLatestStickies(int amount, bool sticky)
        {
            var result = await _httpClient.GetFromJsonAsync<List<NewsBlogResponse>>($"api/newsblog/sticky?amnt={amount}&sticky={sticky}");
            if (result == null)
                return null!;
            else
            {
                return result;
            }
        }

        public async Task<List<NewsBlogResponse>> GetLatestUpdates(string type, int amount, bool sticky)
        {
            var result = await _httpClient.GetFromJsonAsync<List<NewsBlogResponse>>($"api/newsblog/latest/{type}?amnt={amount}&sticky={sticky}");
            if (result == null)
                return null!;
            else
            {
                return result;
            }
        }

        public async Task UpdateArticle(int id, NewsBlogRequest article)
        {
            var updateRequest = new NewsBlogUpdateRequest
            {
                Title = article.Title,
                Slug = article.Slug,
                CoverImageUrl = article.CoverImageUrl,
                StickToFrontPage = article.StickToFrontPage,
                ArticleType = article.ArticleType,
                NewsArticle = article.NewsArticle
            };
            var response = await _httpClient.PutAsJsonAsync($"api/newsblog/{id}", updateRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
