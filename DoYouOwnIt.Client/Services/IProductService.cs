using DoYouOwnIt.Shared.Models.Product;

namespace DoYouOwnIt.Client.Services
{
    public interface IProductService
    {
        event Action? OnChange;
        List<ProductResponse> Products { get; set; }
        Task GetProductsByCategoryId(int categoryId);
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse?> GetProductById(int id);
        Task<ProductResponse?> GetProductBySlug(string slug);
        Task<ProductResponseJustCategoryId?> GetProductByIdJustCategoryId(int id);
        Task<ProductResponseJustCategoryId?> GetProductBySlugJustCategoryId(string slug);
        Task CreateProduct(ProductRequest product);
        Task UpdateProduct(int id, ProductRequest product);
        Task LockProduct(int id, ProductRequest product);
        Task DeleteProduct(int id);
        Task <List<ProductResponse>>SearchProducts(string searchTerm, int? categoryId, int pageNumber, int pageSize);
    }
}
