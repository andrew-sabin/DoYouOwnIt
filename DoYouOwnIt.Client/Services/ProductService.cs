using DoYouOwnIt.Shared.Models.Product;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
        public event Action? OnChange;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProduct(ProductRequest product)
        {
            var createRequest = new ProductCreateRequest
            {
                Name = product.Name,
                Creators = product.Creators,
                CreditsURL = product.CreditsURL,
                ProductLaunchDate = product.ProductLaunchDate,
                CoverImageURL = product.CoverImageURL,
                Description = product.Description,
                IsAIAssisted = product.IsAIAssisted,
                AIAssistsWith = product.AIAssistsWith,
                ForMatureAudiences = product.ForMatureAudiences,
                MatureAudienceReason = product.MatureAudienceReason,
                CategoryId = product.CategoryId
            };
            Console.WriteLine(createRequest);
            await _httpClient.PostAsJsonAsync("api/product", createRequest);
        }

        public async Task DeleteProduct(int id)
        {
            await _httpClient.DeleteAsync($"api/product/{id}");
        }

        public Task<List<ProductResponse>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponse?> GetProductById(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductResponse>($"api/product/{id}");
        }

        public async Task UpdateProduct(int id, ProductRequest product)
        {
            var updateRequest = new ProductCreateRequest
            {
                Name = product.Name,
                Creators = product.Creators,
                CreditsURL = product.CreditsURL,
                ProductLaunchDate = product.ProductLaunchDate,
                CoverImageURL = product.CoverImageURL,
                Description = product.Description,
                IsAIAssisted = product.IsAIAssisted,
                AIAssistsWith = product.AIAssistsWith,
                ForMatureAudiences = product.ForMatureAudiences,
                MatureAudienceReason = product.MatureAudienceReason,
                CategoryId = product.CategoryId
            };
            Console.WriteLine($"Updating product ID: {id}");
            await _httpClient.PutAsJsonAsync($"api/product/{id}", updateRequest);
        }

        public async Task GetProductsByCategoryId(int categoryId)
        {
            List<ProductResponse>? result = null;
            if (categoryId <= 0)
            {
                result = await _httpClient.GetFromJsonAsync<List<ProductResponse>>("api/product");
            }
            else
            {
                result = await _httpClient.GetFromJsonAsync<List<ProductResponse>>($"api/product/category/{categoryId}");
            }

            if (result is not null)
            {
                Products = result;
                OnChange?.Invoke();
            }
            else
            {
                Products = new List<ProductResponse>();
            }
        }

        public async Task<ProductResponseNoCategory?> GetProductByIdNoCategory(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductResponseNoCategory?>($"api/product/{id}");
        }

        public async Task<List<ProductResponse>> SearchProducts(string searchTerm, int? categoryId, int pageNumber, int pageSize)
        {
            var results = await _httpClient.GetFromJsonAsync<List<ProductResponse>>($"api/product/search/{categoryId}/{searchTerm}?pageNumber={pageNumber}&pageSize={pageSize}");
            if (results is null)
            {
                return new List<ProductResponse>();
            }
            return results;
        }

        public async Task<ProductResponse?> GetProductBySlug(string slug)
        {
            return await _httpClient.GetFromJsonAsync<ProductResponse>($"api/product/slug/{slug}");
        }
    }
}