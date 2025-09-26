using DoYouOwnIt.Shared.Models.Product;

namespace DoYouOwnIt.Api.Services
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategoryResponse>> GetAllCategories();
        Task<ProductCategoryResponse?> GetProductCategoryById(int id);
        Task<ProductCategoryResponse?> GetProductCategoryBySlug(string slug);
        Task<List<ProductCategoryResponse>> CreateCategory(ProductCategoryCreateRequest category);
        Task<List<ProductCategoryResponse>?> UpdateCategory(int id, ProductCategoryUpdateRequest category);
        Task<List<ProductCategoryResponse>?> DeleteCategory(int id);
    }
}
