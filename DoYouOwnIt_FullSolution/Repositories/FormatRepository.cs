
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

        public async Task<List<Format>> CreateFormatAsync(Format format)
        {
            format.CreatorId = _userContextService.GetUserId();
            format.LastModified = DateTime.UtcNow;
            format.Slug = SlugHelper.GenerateFormatSlug(format.Type, format.Edition);
            format.ContributerIds.Add(format.CreatorId);
            _context.Formats.Add(format);
            await _context.SaveChangesAsync();
            return await _context.Formats.ToListAsync();
        }

        public async Task<List<Format>?> DeleteFormatAsync(int id)
        {
            var dbFormat = await _context.Formats.FirstOrDefaultAsync(f => f.Id == id);
            if (dbFormat is null)
            {
                return null;
            }
            var productId = dbFormat.ProductId;

            _context.Formats.Remove(dbFormat);
            await _context.SaveChangesAsync();

            return await GetFormatsByProductIdAsync(productId);
        }

        public async Task<List<Format>> GetAllFormatsAsync()
        {
            return await _context.Formats
                .Include(f => f.Product)
                .ToListAsync();
        }

        public async Task<Format?> GetFormatByIdAsync(int id)
        {
            var format = await _context.Formats
                .Include(f => f.Product)
                .FirstOrDefaultAsync(f => f.Id == id);
            return format;
        }

        public async Task<Format?> GetFormatBySlugAsync(string prodSlug, string slug)
        {
            var format = await _context.Formats
                .Include(f => f.Product)
                .Where(f => f.Product.Slug == prodSlug)
                .FirstOrDefaultAsync(f => f.Slug == slug);
            return format;
        }

        public async Task<List<Format>?> GetFormatsByProductIdAsync(int productId)
        {

            return await _context.Formats
                .Where(f => f.ProductId == productId)
                .Include(f => f.Product)
                .ToListAsync();
        }

        public async Task<List<Format>?> UpdateFormatAsync(int id, Format format)
        {
            var dbFormat = await _context.Formats.FirstOrDefaultAsync(f => f.Id == id);
            if (dbFormat is null)
            {
                return null;
            }
            var modifierId = _userContextService.GetUserId();
            format.LastModified = DateTime.UtcNow;
            format.ModifierId = modifierId;
            var IsUserInList = dbFormat.ContributerIds.Any(item => item == modifierId);
            if (!IsUserInList)
            {
                dbFormat.ContributerIds.Add(format.ModifierId);
            }
                
            dbFormat.ModifierId = format.ModifierId;
            dbFormat.LastModified = format.LastModified;

            dbFormat.Type = format.Type;
            dbFormat.Edition = format.Edition;
            if (string.IsNullOrEmpty(format.Slug))
            {
                dbFormat.Slug = SlugHelper.GenerateFormatSlug(format.Type, format.Edition);
            }
            else
            {
                dbFormat.Slug = format.Slug;
            }
            dbFormat.CoverImageUrl = format.CoverImageUrl;
            dbFormat.ReleaseDate = format.ReleaseDate;
            dbFormat.Description = format.Description;
            dbFormat.ProductId = format.ProductId;
            dbFormat.OwnershipLevel = format.OwnershipLevel;
            dbFormat.IsAIAssisted = format.IsAIAssisted;
            dbFormat.AIAssistsWith = format.AIAssistsWith;

            dbFormat.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetFormatsByProductIdAsync(format.ProductId);
        }
    }
}
