
using Mapster;

namespace DoYouOwnIt.Api.Repositories
{
    public class FormatTypeRepository : IFormatTypeRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public FormatTypeRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public Task<FormatType> CreateFormatTypeAsync(FormatType formatType)
        {
            formatType.CreatedDate = DateTime.UtcNow;
            formatType.ModifiedDate = DateTime.UtcNow;
            _context.FormatTypes.Add(formatType);
            return _context.SaveChangesAsync().ContinueWith(t => formatType);
        }

        public async Task<FormatType?> DeleteFormatTypeByIdAsync(int id)
        {
            var dbFormatType = await _context.FormatTypes.FirstOrDefaultAsync(ft => ft.Id == id);
            if (dbFormatType == null)
            {
                return null;
            }
            _context.FormatTypes.Remove(dbFormatType);
            await _context.SaveChangesAsync();

            return await GetFormatTypeByIdAsync(id);
        }

        public async Task<List<FormatType>> GetAllFormatTypesAsync()
        {
            return await _context.FormatTypes
                .Include(ft => ft.Category)
                .ToListAsync();
        }

        public async Task<FormatType?> GetFormatTypeByIdAsync(int id)
        {
            var formatType = await _context.FormatTypes
                .Include(ft => ft.Category)
                .FirstOrDefaultAsync(ft => ft.Id == id);
            return formatType;
        }

        public async Task<List<FormatType>> GetFormatTypesByCategoryId(int categoryId)
        {
            var formatTypes = await _context.FormatTypes
                .Where(ft => ft.CategoryId == categoryId)
                .ToListAsync();
            return formatTypes;
        }

        public async Task<FormatType?> UpdateFormatTypeAsync(FormatType formatType, int id)
        {
            var dbFormatType = await _context.FormatTypes.FirstOrDefaultAsync(ft => ft.Id == id);
            if (dbFormatType == null)
            {
                return null!;
            }
            dbFormatType.Name = formatType.Name;
            dbFormatType.ImageUrl = formatType.ImageUrl;
            dbFormatType.Description = formatType.Description;
            dbFormatType.CategoryId = formatType.CategoryId;
            dbFormatType.ModifiedDate = DateTime.UtcNow;
            dbFormatType.HasIssue = formatType.HasIssue;
            dbFormatType.Issue = formatType.Issue;
            dbFormatType.IssueURL = formatType.IssueURL;

            await _context.SaveChangesAsync();

            return await GetFormatTypeByIdAsync(id);
        }
    }
}
