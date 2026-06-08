using DoYouOwnIt.Shared.Models.Category;

namespace DoYouOwnIt.Client.Services
{
    public interface ICategoryService
    {
        event Action? OnChange;
        public List<CategoryResponse> ProductCategories { get; set; }
        Task LoadAllProductCategories();
        Task LoadAllUnlockedProductCategories();
        Task<List<CategoryResponse>> GetAllProductCategories();
        Task<List<CategoryResponse>> GetAllUnlockedProductCategories();
        Task<CategoryResponse?> GetCategoryById(int id);
        Task<CategoryResponse?> GetCategoryBySlug(string slug);
        Task CreateCategory(CategoryRequest Category);
        Task UpdateCategoryByID(int id, CategoryRequest Category);
        Task<List<CategoryResponse>?> UpdateCategoryBySlug(string slug, CategoryRequest Category);
        Task<List<CategoryResponse>?> DeleteCategory(int id);
        Task LockCategory(int id, CategoryRequest Category);
    }
}
