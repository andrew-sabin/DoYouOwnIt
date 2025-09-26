namespace DoYouOwnIt.Api.Services
{
    public interface IFormatService
    {
        Task<List<FormatResponse>> GetAllFormatsAsync();
        Task<FormatResponse?> GetFormatByIdAsync(int id);
        Task<FormatResponse?> GetFormatBySlugAsync(string prodSlug, string slug);
        Task<List<FormatResponse>?> GetFormatsByProductIdAsync(int productId);
        Task<List<FormatResponse>> CreateFormatAsync(FormatCreateRequest format);
        Task<List<FormatResponse>?> UpdateFormatAsync(int id, FormatUpdateRequest format);
        Task<List<FormatResponse>?> DeleteFormatAsync(int id);
    }
}
