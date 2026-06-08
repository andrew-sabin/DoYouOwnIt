using DoYouOwnIt.Api.Repositories.Interfaces;
using DoYouOwnIt.Shared.Models.FormatRevision;
using DoYouOwnIt_Shared.Entities.Revisions;
using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class FormatRevisionService : IFormatRevisionService
    {
        private readonly IFormatRevisionRepository _formatRevisionRepo;
        public FormatRevisionService(IFormatRevisionRepository formatRevisionRepository)
        {
            _formatRevisionRepo = formatRevisionRepository;
        }

        public async Task<FormatRevisionResponse> CreateNewFormatRevisionAsync(FormatRevisionCreateRequest formatRevisionCreateRequest)
        {
            var newEntry = formatRevisionCreateRequest.Adapt<FormatRevision>();
            var result = await _formatRevisionRepo.CreateNewFormatRevisionAsync(newEntry);
            return result.Adapt<FormatRevisionResponse>();
        }

        public async Task<List<FormatRevisionResponse>?> DeleteFormatRevisionAsync(int formatRevisionId)
        {
            var result = await _formatRevisionRepo.DeleteFormatRevisionAsync(formatRevisionId);
            if (result == null)
                return null;

            return result.Adapt<List<FormatRevisionResponse>>();
        }

        public async Task<FormatRevisionResponse?> GetFormatRevisionByIdAsync(int formatRevisionId)
        {
            var result = await _formatRevisionRepo.GetFormatRevisionByIdAsync(formatRevisionId);
            if (result is null)
                return null;

            return result.Adapt<FormatRevisionResponse>();
        }

        public async Task<List<FormatRevisionResponse>> GetFormatRevisionsAsync()
        {
            var result = await _formatRevisionRepo.GetAllFormatRevisions();
            return result.Adapt<List<FormatRevisionResponse>>();
        }

        public async Task<List<FormatRevisionResponse>> GetFormatRevisionsByFormatId(int formatId)
        {
            var result = await _formatRevisionRepo.GetFormatRevisionsByFormatId(formatId);
            return result.Adapt<List<FormatRevisionResponse>>();
        }

        public async Task<FormatRevisionResponse?> GetLatestFormatRevisionByFormatId(int formatId)
        {
            var result = await _formatRevisionRepo.GetMostRecentRevisionByFormatId(formatId);
            if (result is null)
                return null;

            return result.Adapt<FormatRevisionResponse>();
        }
    }
}
