namespace DoYouOwnIt.Api.Repositories
{
    public interface IFormatTypeRepository
    {
        Task<List<FormatType>> GetAllFormatTypesAsync();
        Task<FormatType?> GetFormatTypeByIdAsync(int id);
        Task<List<FormatType>> GetFormatTypesByCategoryId(int categoryId);
        Task<FormatType> CreateFormatTypeAsync(FormatType formatType);
        Task<FormatType?> UpdateFormatTypeAsync(FormatType formatType, int id);
        Task<FormatType?> DeleteFormatTypeByIdAsync(int id);

    }
}
