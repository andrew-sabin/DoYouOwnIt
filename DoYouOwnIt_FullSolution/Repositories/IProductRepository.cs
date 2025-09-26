namespace DoYouOwnIt.Api.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product?> GetProductBySlug(string slug);
        Task<List<Product>> GetProductByCategoryId(int categoryId);
        Task<List<Product>> CreateProduct(Product product);
        Task<List<Product>?> UpdateProduct(int id, Product product);
        Task<List<Product>?> DeleteProduct(int id);
        Task<List<Product>> SearchProducts(string searchTerm, int? categoryId, int pageNumber, int pageSize);
    }
}
