using DoYouOwnIt.Shared.Models.Format;

namespace DoYouOwnIt.Client.Services
{
    public interface IFormatService
    {
        event Action? OnChange;
        List<FormatResponse> Formats { get; set; }
        Task GetFormatsByProductId (int productId);
        Task<FormatResponse?> GetFormatByID(int formatId);
        Task CreateFormat (FormatRequest format);
        Task UpdateFormat (int formatID, FormatRequest format);
        Task DeleteFormat (int formatID);

    }
}
