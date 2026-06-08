namespace DoYouOwnIt.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Category>> GetAllUnlockedCategories();
        Task<Category?> GetCategoryById(int id);
        Task<Category?> GetCategoryBySlug(string slug);
        Task<List<Category>> CreateCategory(Category category);
        Task<List<Category>?> UpdateCategory(int id, Category category);
        Task<Category?> LockCategory(int id, Category category);
        Task<List<Category>?> DeleteCategory(int id);
    }
}
