namespace DoYouOwnIt.Api.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse?> GetProductById(int id);
        Task<ProductResponse?> GetProductBySlug(string slug);
        Task<List<ProductResponse>?> GetProductsByCategoryId(int categoryId);
        Task<List<ProductResponse>> CreateProduct(ProductCreateRequest product);
        Task<List<ProductResponse>?> UpdateProduct(int id, ProductUpdateRequest product);
        Task<List<ProductResponse>?> DeleteProduct(int id);
        Task<List<ProductResponse>> SearchProducts(string searchTerm, int? categoryId, int pageNumber, int pageSize);
    }
}
