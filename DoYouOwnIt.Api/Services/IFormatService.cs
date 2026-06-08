namespace DoYouOwnIt.Api.Services
{
    public interface IFormatService
    {
        Task<List<FormatResponse>> GetAllFormatsAsync();
        Task<List<FormatResponse>> GetRecentFormatsAsync(int amount);
        Task<List<FormatResponse>> GetRecentlyUpdatedFormatsAsync(int amount);
        Task<FormatResponse?> GetFormatByIdAsync(int id);
        Task<FormatResponse?> GetFormatByIdAdminAsync(int id);
        Task<FormatResponse?> GetFormatBySlugAsync(string prodSlug, string slug);
        Task<FormatResponse?> GetFormatBySlugAdminAsync(string prodSlug, string slug);
        Task<List<FormatResponse>?> GetFormatsByProductIdAsync(int productId);
        Task<List<FormatResponse>?> GetFormatsByProductIdAdminAsync(int productId);
        Task<FormatResponse> CreateFormatAsync(FormatCreateRequest format);
        Task<FormatResponse?> UpdateFormatAsync(int id, FormatUpdateRequest format);
        Task<FormatResponse?> LockFormatAsync(int id, FormatLockRequest format);
        Task<List<FormatResponse>?> DeleteFormatAsync(int id);
    }
}
