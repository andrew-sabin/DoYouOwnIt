using DoYouOwnIt.Shared.Models.ProductCategory;

namespace DoYouOwnIt.Client.Services
{
    public interface IProductCategoryService
    {
        event Action? OnChange;
        public List<ProductCategoryResponse> ProductCategories { get; set; }
        Task LoadAllProductCategories();
        Task<List<ProductCategoryResponse>> GetAllProductCategories();
        Task<ProductCategoryResponse?> GetProductCategoryById(int id);
        Task<ProductCategoryResponse?> GetProductCategoryBySlug(string slug);
        Task CreateProductCategory(ProductCategoryRequest productCategory);
        Task UpdateProductCategoryByID(int id, ProductCategoryRequest productCategory);
        Task<List<ProductCategoryResponse>?> UpdateProductCategoryBySlug(string slug, ProductCategoryRequest productCategory);
        Task<List<ProductCategoryResponse>?> DeleteProductCategory(int id);
    }
}
