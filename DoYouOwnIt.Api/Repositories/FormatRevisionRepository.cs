using DoYouOwnIt.Api.Repositories.Interfaces;
using DoYouOwnIt_Shared.Entities.Revisions;

namespace DoYouOwnIt.Api.Repositories
{
    public class FormatRevisionRepository : IFormatRevisionRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public FormatRevisionRepository (DataContext dataContext, IUserContextService userContextService)
        {
            _context = dataContext;
            _userContextService = userContextService;
        }

        public async Task<FormatRevision> CreateNewFormatRevisionAsync(FormatRevision revision)
        {
            revision.ModifierId = _userContextService.GetUserId();
            revision.ModifierName = _userContextService.GetUserName();
            revision.ModifiedDate = DateTime.UtcNow;
            _context.FormatRevisions.Add(revision);
            await _context.SaveChangesAsync();
            return revision;
        }

        public async Task<List<FormatRevision>?> DeleteFormatRevisionAsync(int revisionId)
        {
            var result = _context.FormatRevisions.FirstOrDefault(r => r.Id == revisionId);
            if (result == null)
            {
                return null;
            }

            //var dbFormat = result.FormatId
            _context.FormatRevisions.Remove(result);
            await _context.SaveChangesAsync();
            return await GetAllFormatRevisions();
        }

        public async Task<List<FormatRevision>> GetAllFormatRevisions()
        {
            return await _context.FormatRevisions
                .Include(fr => fr.Format)
                .Include(fr => fr.PreviousRevision)
                .ToListAsync();
        }

        public async Task<FormatRevision?> GetFormatRevisionByIdAsync(int revisionId)
        {
            var result = await _context.FormatRevisions
                .Include(fr => fr.Format)
                .Include(fr => fr.PreviousRevision)
                .FirstOrDefaultAsync(fr => fr.Id == revisionId);

            return result;
        }

        public async Task<List<FormatRevision>> GetFormatRevisionsByFormatId(int formatId)
        {
            return await _context.FormatRevisions
                .Where(fr => fr.FormatId == formatId)
                .OrderByDescending(fr => fr.ModifiedDate)
                .Include(fr => fr.Format)
                .ToListAsync();
        }

        public async Task<FormatRevision?> GetMostRecentRevisionByFormatId(int formatId)
        {
            var result = await _context.FormatRevisions
                .Include(fr => fr.Format)
                .OrderBy(fr => fr.Id)
                .LastOrDefaultAsync(fr => fr.FormatId == formatId);

            if (result == null)
                return null;

            return result;
        }
    }
}
