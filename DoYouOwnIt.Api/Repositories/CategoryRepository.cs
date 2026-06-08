using DoYouOwnIt.Shared.Helpers;

namespace DoYouOwnIt.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public CategoryRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }
        public async Task<List<Category>> CreateCategory(Category category)
        {
            category.Slug = SlugHelper.GenerateSlug(category.Name);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>?> DeleteCategory(int id)
        {
            var dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null)
            {
                return null;
            }

            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();

            return await GetAllCategories();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetAllUnlockedCategories()
        {
            return await _context.Categories.Where(c => !c.IsLocked).ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public Task<Category?> GetCategoryBySlug(string slug)
        {
            var category = _context.Categories
                .FirstOrDefaultAsync(c => c.Slug == slug);
            return category;
        }

        public async Task<Category?> LockCategory(int id, Category category)
        {
            var dbCategory = await _context.Categories.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
            if (dbCategory == null)
            {
                return null;
            }

            if (category.IsLocked)
            {
                var modId = _userContextService.GetUserId();
                dbCategory.IsLocked = true;
                dbCategory.LockedByUser = modId;
                dbCategory.lockedReason = category.lockedReason;
                dbCategory.LockedDate = DateTime.UtcNow;
            }
            else
            {
                dbCategory.IsLocked = false;
                dbCategory.LockedByUser = "";
                dbCategory.lockedReason = category.lockedReason;
                dbCategory.LockedDate = null;
            }

            await _context.SaveChangesAsync();
            return dbCategory;
        }

        public async Task<List<Category>?> UpdateCategory(int id, Category category)
        {
            var dbCategory = await _context.Categories.FindAsync(id);
            if(dbCategory == null)
            {
                return null;
            }

            dbCategory.Name = category.Name;
            if (string.IsNullOrEmpty(dbCategory.Slug) && string.IsNullOrEmpty(category.Slug))
            {
                dbCategory.Slug = SlugHelper.GenerateSlug(category.Name);
            }
            else if (!string.IsNullOrEmpty(category.Slug))
            {
                dbCategory.Slug = category.Slug;
            }
            dbCategory.Description = category.Description;
            dbCategory.CreatorsTitle = category.CreatorsTitle;
            dbCategory.FormatsTitle = category.FormatsTitle;
            dbCategory.TypeTitle = category.TypeTitle;
            dbCategory.EditionTitle = category.EditionTitle;
            dbCategory.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetAllCategories();
        }
    }
}
