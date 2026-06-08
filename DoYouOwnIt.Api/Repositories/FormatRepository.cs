

using Blazorise.Extensions;

namespace DoYouOwnIt.Api.Repositories
{
    public class FormatRepository : IFormatRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public FormatRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<Format> CreateFormatAsync(Format format)
        {
            format.CreatorId = _userContextService.GetUserId();
            format.ModifiedDate = DateTime.UtcNow;
            format.CreatorName = _userContextService.GetUserName();
            format.ModifierName = _userContextService.GetUserName();
            format.ModifierId = _userContextService.GetUserId();
            format.Slug = SlugHelper.GenerateFormatSlug(format.Type!, format.Edition!, format.ReleaseDate, format.ProductId);
            _context.Formats.Add(format);
            await _context.SaveChangesAsync();
            return format;
        }

        public async Task<List<Format>?> DeleteFormatAsync(int id)
        {
            var dbFormat = await _context.Formats.FirstOrDefaultAsync(f => f.Id == id);
            if (dbFormat is null)
            {
                return null;
            }
            var productId = dbFormat.ProductId;

            if (dbFormat.IsDeleted == true)
            {
                dbFormat.IsDeleted = false;
                dbFormat.DeletedDate = null;
            }
            else
            {
                dbFormat.IsDeleted = true;
                dbFormat.DeletedDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return await GetFormatsByProductIdAsync(productId);
        }

        public async Task<List<Format>> GetAllFormatsAsync()
        {
            return await _context.Formats
                .Include(f => f.Product)
                .Where(f => f.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<Format?> GetFormatByIdAdmin(int id)
        {
            var format = await _context.Formats
                .Include(f => f.Product)
                .FirstOrDefaultAsync(f => f.Id == id);
            return format;
        }

        public async Task<Format?> GetFormatByIdAsync(int id)
        {
            var format = await _context.Formats
                .Include(f => f.Product)
                .FirstOrDefaultAsync(f => f.Id == id && f.IsDeleted != true);
            return format;
        }

        public async Task<List<Format>?> GetFormatByProductIdAdminAsync(int productId)
        {
            return await _context.Formats
                .Where(f => f.ProductId == productId)
                .Include(f => f.Product)
                .ToListAsync();
        }

        public async Task<Format?> GetFormatBySlugAdminAsync(string prodSlug, string slug)
        {
            var format = await _context.Formats
                .Include(f => f.Product)
                .Where(f => f.Product!.Slug == prodSlug)
                .FirstOrDefaultAsync(f => f.Slug == slug);
            return format;
        }

        public async Task<Format?> GetFormatBySlugAsync(string prodSlug, string slug)
        {
            var format = await _context.Formats
                .Include(f => f.Product)
                .Where(f => f.Product!.Slug == prodSlug && f.IsDeleted != true)
                .FirstOrDefaultAsync(f => f.Slug == slug);
            return format;
        }

        public async Task<List<Format>?> GetFormatsByProductIdAsync(int productId)
        {

            return await _context.Formats
                .Where(f => f.ProductId == productId && f.IsDeleted != true)
                .Include(f => f.Product)
                .ToListAsync();
        }

        public async Task<List<Format>> GetRecentFormats(int amount)
        {
            return await _context.Formats.OrderByDescending(f => f.CreatedDate)
                .Include(f => f.Product)
                .Where(f => f.IsDeleted != true)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<List<Format>> GetRecentlyUpdatedFormats(int amount)
        {
            return await _context.Formats.OrderByDescending(f => f.ModifiedDate)
                .Include(f => f.Product)
                .Where(f => f.IsDeleted != true)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<Format?> LockFormatAsync(int id, Format format)
        {
            var dbFormat = await _context.Formats.FirstOrDefaultAsync(f => f.Id == id);
            if (dbFormat is null)
            {
                return null;
            }
            
            if (!format.IsLocked)
            {
                dbFormat.LockedByUser = "";
                dbFormat.IsLocked = format.IsLocked;
                dbFormat.lockedReason = "";
                dbFormat.LockedDate = null;
            }
            else
            {
                var modId = _userContextService.GetUserId();
                dbFormat.LockedByUser = modId;
                dbFormat.IsLocked = format.IsLocked;
                dbFormat.lockedReason = format.lockedReason;
                dbFormat.LockedDate = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return format;
        }

        public async Task<Format?> UpdateFormatAsync(int id, Format format)
        {
            var dbFormat = await _context.Formats.FirstOrDefaultAsync(f => f.Id == id);
            if (dbFormat is null || dbFormat.IsDeleted)
            {
                return null;
            }
            format.ModifiedDate = DateTime.UtcNow;
            dbFormat.ModifiedDate = format.ModifiedDate;
            dbFormat.ModifierName = _userContextService.GetUserName();
            dbFormat.ModifierId = _userContextService.GetUserId();
            if (string.IsNullOrEmpty(format.Slug))
            {
                dbFormat.Slug = SlugHelper.GenerateFormatSlug(format.Type!, format.Edition!, format.ReleaseDate, format.ProductId);
            }
            else
            {
                dbFormat.Slug = format.Slug;
            }
            dbFormat.CoverImageUrl = format.CoverImageUrl;
            dbFormat.ProductId = format.ProductId;
            dbFormat.ModifiedDate = DateTime.UtcNow;
            dbFormat.formatrevisionid = format.formatrevisionid;


            await _context.SaveChangesAsync();
            return dbFormat;
        }
    }
}
