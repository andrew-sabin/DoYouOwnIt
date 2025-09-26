namespace DoYouOwnIt.Api.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAllCategories();
        Task<ProductCategory?> GetCategoryById(int id);
        Task<ProductCategory?> GetCategoryBySlug(string slug);
        Task<List<ProductCategory>> CreateCategory(ProductCategory category);
        Task<List<ProductCategory>?> UpdateCategory(int id, ProductCategory category);
        Task<List<ProductCategory>?> DeleteCategory(int id);
    }
}
