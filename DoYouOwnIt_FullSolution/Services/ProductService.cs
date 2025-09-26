
using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> CreateProduct(ProductCreateRequest product)
        {
            var newEntry = product.Adapt<Product>();
            var result = await _productRepository.CreateProduct(newEntry);
            return result.Adapt<List<ProductResponse>>();
        }

        public async Task<List<ProductResponse>?> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProduct(id);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<ProductResponse>>();
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var result = await _productRepository.GetAllProducts();
            return result.Adapt<List<ProductResponse>>();
        }

        public async Task<List<ProductResponse>?> GetProductsByCategoryId(int categoryId)
        {
            //throw new NotImplementedException();
            var results = await _productRepository.GetProductByCategoryId(categoryId);
            return results.Adapt<List<ProductResponse>?>();
        }

        public async Task<ProductResponse?> GetProductById(int id)
        {
            var result = await _productRepository.GetProductById(id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<ProductResponse>();
        }

        public async Task<List<ProductResponse>?> UpdateProduct(int id, ProductUpdateRequest request)
        {
            var updatedProduct = request.Adapt<Product>();
            var result = await _productRepository.UpdateProduct(id, updatedProduct);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<ProductResponse>>();

        }

        public async Task<List<ProductResponse>> SearchProducts(string searchTerm, int? categoryId, int pageNumber, int pageSize)
        {
            var results = await _productRepository.SearchProducts(searchTerm, categoryId, pageNumber, pageSize);
            return results.Adapt<List<ProductResponse>>();
        }

        public async Task<ProductResponse?> GetProductBySlug(string slug)
        {
            var result = await _productRepository.GetProductBySlug(slug);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<ProductResponse>();
        }
    }
}
