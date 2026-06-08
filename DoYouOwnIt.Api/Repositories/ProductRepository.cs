using DoYouOwnIt.Shared.Entities;

namespace DoYouOwnIt.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public ProductRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<Product>> GetAllProducts()
        {

            var products = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .ToListAsync();
            return products;
        }
        public async Task<Product?> GetProductById(int id)
        {
            var product = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }
        public async Task<List<Product>> CreateProduct(Product product)
        {
            var creatorId = _userContextService.GetUserId();
            product.Slug = SlugHelper.GenerateProductSlug(product.Name, product.ProductLaunchDate, product.CategoryId);
            product.CreatorId = creatorId;
            product.ModifiedDate = DateTime.UtcNow;
            product.ModifierId = creatorId;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return await _context.Products.Where(p => !p.IsDeleted).ToListAsync();
        }
        public async Task<List<Product>?> UpdateProduct(int id, Product product)
        {
            var dbProduct = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);
            if (dbProduct == null)
            {
                return null;
            }
            product.ModifierId = _userContextService.GetUserId();
            product.ModifiedDate = DateTime.UtcNow;

            dbProduct.Name = product.Name;
            dbProduct.CoverImageURL = product.CoverImageURL;
            dbProduct.ProductLaunchDate = product.ProductLaunchDate;
            dbProduct.Description = product.Description;
            dbProduct.DescriptionSource = product.DescriptionSource;
            dbProduct.CategoryId = product.CategoryId;
            if (string.IsNullOrEmpty(product.Slug))
            {
                dbProduct.Slug = SlugHelper.GenerateProductSlug(product.Name, product.ProductLaunchDate, product.CategoryId);
            }
            else
            {
                dbProduct.Slug = product.Slug;
            }
            dbProduct.IsLocked = product.IsLocked;
            dbProduct.Creators = product.Creators;
            dbProduct.ContentRating = product.ContentRating;
            dbProduct.IsAIAssisted = product.IsAIAssisted;
            dbProduct.AIAssistsWith = product.AIAssistsWith;
            dbProduct.ForMatureAudiences = product.ForMatureAudiences;
            dbProduct.MatureAudienceReason = product.MatureAudienceReason;
            dbProduct.CategoryName = product.CategoryName;
            dbProduct.ModifierId = product.ModifierId;
            dbProduct.ModifiedDate = product.ModifiedDate;
            dbProduct.HasIssue = product.HasIssue;
            dbProduct.Issue = product.Issue;
            dbProduct.IssueURL = product.IssueURL;

            await _context.SaveChangesAsync();
            return await GetAllProducts();
        }
        public async Task<List<Product>?> DeleteProduct(int id)
        {
            var dbProduct = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);
            if (dbProduct == null)
            {
                return null;
            }

            dbProduct.IsDeleted = true;
            dbProduct.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetAllProducts();
        }

        public async Task<List<Product>> GetProductByCategoryId(int categoryId)
        {
            return await _context.Products
                .Where(p => !p.IsDeleted && p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<List<Product>> SearchProducts(string searchTerm, int? categoryId, int pageNumber, int pageSize)
        {
            var query = _context.Products
                .Where(p => !p.IsDeleted && (p.Name.Contains(searchTerm) ||
                p.Creators!.Contains(searchTerm) || p.Description!.Contains(searchTerm)));
            var totalCount = query.Count();
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }
            var skipNumber = (pageNumber - 1) * pageSize;
            query = query.Include(p => p.Category);

            return await query.Skip(skipNumber).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetProductBySlug(string slug)
        {
            var product = await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Slug == slug);
            return product;
        }

        public async Task<List<Product>> GetProductsByCreatorId(string userId)
        {
            var products = await _context.Products.Where(p => !p.IsDeleted)
                .Where(p => p.CreatorId == userId)
                .ToListAsync();

            return products;
        }

        public Task<List<Product>> GetProductsByEditorId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> LockProduct(int id, Product product)
        {
            var dbProduct = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);
            if (dbProduct == null)
            {
                return null;
            }
            if (product.IsLocked)
            {
                var modId = _userContextService.GetUserId();
                dbProduct.LockedByUser = _userContextService.GetUserId();
                dbProduct.IsLocked = product.IsLocked;
                dbProduct.lockedReason = product.lockedReason;
                dbProduct.LockedByUser = modId;
                dbProduct.LockedDate = DateTime.UtcNow;

            }
            else
            {
                dbProduct.LockedByUser = "";
                dbProduct.lockedReason = "";
                dbProduct.IsLocked = false;
                dbProduct.LockedDate = null;
            }
            await _context.SaveChangesAsync();
            return dbProduct;

        }
    }
}
