using DoYouOwnIt.Shared.Models.Format;

namespace DoYouOwnIt.Client.Services
{
    public interface IFormatService
    {
        event Action? OnChange;
        List<FormatResponse> Formats { get; set; }
        Task <List<FormatResponse>>GetFormatsByProductId (int productId);
        Task<List<FormatResponse>> GetRecentFormats(int amount);
        Task<List<FormatResponse>> GetRecentlyUpdatedFormats(int amount);
        Task<FormatResponse?> GetFormatByID(int formatId);
        Task<FormatResponse> CreateFormat (FormatRequest format);
        Task<FormatResponse> UpdateFormat (int formatID, FormatRequest format);
        Task LockFormat (int formatId, FormatRequest format);
        Task DeleteFormat (int formatID);

    }
}
