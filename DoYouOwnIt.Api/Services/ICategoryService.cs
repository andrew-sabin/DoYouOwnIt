using DoYouOwnIt.Shared.Models.Product;

namespace DoYouOwnIt.Api.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<List<CategoryResponse>> GetAllUnlockedCategories();
        Task<CategoryResponse?> GetCategoryById(int id);
        Task<CategoryResponse?> GetCategoryBySlug(string slug);
        Task<List<CategoryResponse>> CreateCategory(CategoryCreateRequest category);
        Task<List<CategoryResponse>?> UpdateCategory(int id, CategoryUpdateRequest category);
        Task<CategoryResponse?> LockCategory(int id, CategoryLockRequest category);
        Task<List<CategoryResponse>?> DeleteCategory(int id);
    }
}
