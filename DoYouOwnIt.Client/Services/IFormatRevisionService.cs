using DoYouOwnIt.Shared.Models.FormatRevision;

namespace DoYouOwnIt.Client.Services
{
    public interface IFormatRevisionService
    {
        event Action? OnChange;
        List<FormatRevisionResponse> FormatRevisions { get; set; }
        Task <List<FormatRevisionResponse>> GetRevisionsByFormatId(int formatId);
        Task<FormatRevisionResponse?> GetMostRecentRevision(int formatId);
        Task<FormatRevisionResponse?> GetRevisionById(int revisionId);
        Task<FormatRevisionResponse> CreateRevision(FormatRevisionRequest request);
        Task DeleteRevision(int revisionId);
    }
}
