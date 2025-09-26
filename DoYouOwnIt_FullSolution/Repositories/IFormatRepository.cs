namespace DoYouOwnIt.Api.Repositories
{
    public interface IFormatRepository
    {
        Task <List<Format>> GetAllFormatsAsync();
        Task <Format?> GetFormatByIdAsync(int id);
        Task <Format?> GetFormatBySlugAsync(string prodSlug, string slug);
        Task <List<Format>> CreateFormatAsync(Format format);
        Task <List<Format>?> UpdateFormatAsync(int id, Format format);
        Task<List<Format>?> DeleteFormatAsync(int id);
        Task<List<Format>?> GetFormatsByProductIdAsync(int productId);
    }
}