namespace DoYouOwnIt.Api.Repositories
{
    public interface IFormatRepository
    {
        Task <List<Format>> GetAllFormatsAsync();
        Task <List<Format>> GetRecentFormats(int amount);
        Task<List<Format>> GetRecentlyUpdatedFormats(int amount);
        Task <Format?> GetFormatByIdAsync(int id);
        Task <Format?> GetFormatByIdAdmin(int id);
        Task <Format?> GetFormatBySlugAsync(string prodSlug, string slug);
        Task<Format?> GetFormatBySlugAdminAsync(string prodSlug, string slug);
        Task<Format> CreateFormatAsync(Format format);
        Task <Format?> UpdateFormatAsync(int id, Format format);
        Task<Format?> LockFormatAsync(int id, Format format);
        Task<List<Format>?> DeleteFormatAsync(int id);
        Task<List<Format>?> GetFormatsByProductIdAsync(int productId);
        Task<List<Format>?> GetFormatByProductIdAdminAsync(int productId);
    }
}