using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> CreateCategory(CategoryCreateRequest category)
        {
            var newEntry = category.Adapt<Category>();
            var result = await _categoryRepository.CreateCategory(newEntry);
            return result.Adapt<List<CategoryResponse>>();
        }

        public async Task<List<CategoryResponse>?> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategory(id);
            if(result == null)
            {
                return null;
            }
            return result.Adapt<List<CategoryResponse>>();
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var result = await _categoryRepository.GetAllCategories();
            return result.Adapt<List<CategoryResponse>>();
        }

        public async Task<List<CategoryResponse>> GetAllUnlockedCategories()
        {
            var result = await _categoryRepository.GetAllUnlockedCategories();
            return result.Adapt<List<CategoryResponse>>();
        }

        public async Task<CategoryResponse?> GetCategoryById(int id)
        {
            var result = await _categoryRepository.GetCategoryById(id);
            if(result is null)
            {
                return null;
            }
            return result.Adapt<CategoryResponse>();
        }

        public async Task<CategoryResponse?> GetCategoryBySlug(string slug)
        {
            var result = await _categoryRepository.GetCategoryBySlug(slug);
            return result.Adapt<CategoryResponse?>();
        }

        public async Task<CategoryResponse?> LockCategory(int id, CategoryLockRequest category)
        {
            var lockCategory = category.Adapt<Category>();
            var result = await _categoryRepository.LockCategory(id, lockCategory);
            if(result == null)
            {
                return null;
            }
            return result.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>?> UpdateCategory(int id, CategoryUpdateRequest request)
        {
            var updatedCategory = request.Adapt<Category>();
            var result = await _categoryRepository.UpdateCategory(id, updatedCategory);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<CategoryResponse>>();
        }
    }
}
