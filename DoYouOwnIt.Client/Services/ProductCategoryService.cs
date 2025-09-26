using DoYouOwnIt.Shared.Models.ProductCategory;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly HttpClient _httpClient;
        public List<ProductCategoryResponse> ProductCategories { get; set; } = new List<ProductCategoryResponse>();

        public event Action? OnChange;

        public ProductCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task CreateProductCategory(ProductCategoryRequest productCategory)
        {
            var createRequest = new ProductCategoryCreateRequest
            {
                Category = productCategory.Category,
                Description = productCategory.Description
            };
            Console.WriteLine($"Creating product category: {productCategory.Category}");
            return _httpClient.PostAsJsonAsync("api/productcategory", createRequest);
        }

        public Task<List<ProductCategoryResponse>?> DeleteProductCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductCategoryResponse?> GetProductCategoryById(int id)
        {
            var response = await _httpClient.GetAsync($"api/productcategory/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await _httpClient.GetFromJsonAsync<ProductCategoryResponse>($"api/productcategory/{id}");
        }

        public async Task UpdateProductCategoryByID(int id, ProductCategoryRequest productCategory)
        {
            var updateRequest = new ProductCategoryRequest
            {
                Category = productCategory.Category,
                Slug = productCategory.Slug,
                Description = productCategory.Description
            };
            Console.WriteLine($"Updating product category with ID: {id}");
            await _httpClient.PutAsJsonAsync($"api/productcategory/{id}", updateRequest);
        }

        public async Task LoadAllProductCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductCategoryResponse>>("api/productcategory");
            if(result is not null)
            {
                ProductCategories = result;
                OnChange?.Invoke();
            }
            else
            {
                ProductCategories = new List<ProductCategoryResponse>();
            }
        }

        public async Task<ProductCategoryResponse?> GetProductCategoryBySlug(string slug)
        {
            var response = await _httpClient.GetAsync($"api/productcategory/slug/{slug}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await _httpClient.GetFromJsonAsync<ProductCategoryResponse>($"api/productcategory/slug/{slug}");
        }

        public Task<List<ProductCategoryResponse>?> UpdateProductCategoryBySlug(string slug, ProductCategoryRequest productCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductCategoryResponse>> GetAllProductCategories()
        {
            var results = await _httpClient.GetFromJsonAsync<List<ProductCategoryResponse>>("api/productcategory");
            if(results is null)
            {
              return new List<ProductCategoryResponse>();
            }
            return results;
        }
    }
}
