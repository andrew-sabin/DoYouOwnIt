using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryService(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ProductCategoryResponse>> CreateCategory(ProductCategoryCreateRequest category)
        {
            var newEntry = category.Adapt<ProductCategory>();
            var result = await _categoryRepository.CreateCategory(newEntry);
            return result.Adapt<List<ProductCategoryResponse>>();
        }

        public async Task<List<ProductCategoryResponse>?> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategory(id);
            if(result == null)
            {
                return null;
            }
            return result.Adapt<List<ProductCategoryResponse>>();
        }

        public async Task<List<ProductCategoryResponse>> GetAllCategories()
        {
            var result = await _categoryRepository.GetAllCategories();
            return result.Adapt<List<ProductCategoryResponse>>();
        }

        public async Task<ProductCategoryResponse?> GetProductCategoryById(int id)
        {
            var result = await _categoryRepository.GetCategoryById(id);
            if(result is null)
            {
                return null;
            }
            return result.Adapt<ProductCategoryResponse>();
        }

        public async Task<ProductCategoryResponse?> GetProductCategoryBySlug(string slug)
        {
            var result = await _categoryRepository.GetCategoryBySlug(slug);
            return result.Adapt<ProductCategoryResponse?>();
        }

        public async Task<List<ProductCategoryResponse>?> UpdateCategory(int id, ProductCategoryUpdateRequest request)
        {
            var updatedCategory = request.Adapt<ProductCategory>();
            var result = await _categoryRepository.UpdateCategory(id, updatedCategory);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<ProductCategoryResponse>>();
        }
    }
}
