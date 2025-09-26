using DoYouOwnIt.Shared.Helpers;

namespace DoYouOwnIt.Api.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DataContext _context;

        public ProductCategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ProductCategory>> CreateCategory(ProductCategory category)
        {
            category.Slug = SlugHelper.GenerateSlug(category.Category);
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();

            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<List<ProductCategory>?> DeleteCategory(int id)
        {
            var dbProductCategory = await _context.ProductCategories.FindAsync(id);
            if (dbProductCategory == null)
            {
                return null;
            }

            _context.ProductCategories.Remove(dbProductCategory);
            await _context.SaveChangesAsync();

            return await GetAllCategories();
        }

        public async Task<List<ProductCategory>> GetAllCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetCategoryById(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            return category;
        }

        public Task<ProductCategory?> GetCategoryBySlug(string slug)
        {
            var category = _context.ProductCategories
                .FirstOrDefaultAsync(c => c.Slug == slug);
            return category;
        }

        public async Task<List<ProductCategory>?> UpdateCategory(int id, ProductCategory category)
        {
            var dbProductCategory = await _context.ProductCategories.FindAsync(id);
            if(dbProductCategory == null)
            {
                return null;
            }

            dbProductCategory.Category = category.Category;
            if (string.IsNullOrEmpty(dbProductCategory.Slug) && string.IsNullOrEmpty(category.Slug))
            {
                dbProductCategory.Slug = SlugHelper.GenerateSlug(category.Category);
            }
            else if (!string.IsNullOrEmpty(category.Slug))
            {
                dbProductCategory.Slug = category.Slug;
            }
            dbProductCategory.Description = category.Description;
            dbProductCategory.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetAllCategories();
        }
    }
}
