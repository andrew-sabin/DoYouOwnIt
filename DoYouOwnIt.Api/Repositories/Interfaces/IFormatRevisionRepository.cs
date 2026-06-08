using DoYouOwnIt_Shared.Entities.Revisions;

namespace DoYouOwnIt.Api.Repositories.Interfaces
{
    public interface IFormatRevisionRepository
    {
        Task<List<FormatRevision>> GetAllFormatRevisions();
        Task <FormatRevision?> GetFormatRevisionByIdAsync (int revisionId);
        Task <FormatRevision> CreateNewFormatRevisionAsync(FormatRevision revision);
        Task <List<FormatRevision>?> DeleteFormatRevisionAsync(int revisionId);
        Task <List<FormatRevision>> GetFormatRevisionsByFormatId(int formatId);
        Task<FormatRevision?> GetMostRecentRevisionByFormatId(int formatId);
    }
}
