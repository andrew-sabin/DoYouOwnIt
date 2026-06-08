namespace DoYouOwnIt.Api.Services
{
    public interface IFormatTypeService
    {
        Task<List<FormatTypeResponse>> GetAllFormatTypesAsync();
        Task<FormatTypeResponse> CreateFormatTypeAsync(FormatTypeCreateRequest formatType);
        Task<FormatTypeResponse?> GetFormatTypeByIdAsync(int id);
        Task<List<FormatTypeResponse>> GetFormatTypesByCategoryId(int categoryId);
        Task<FormatTypeResponse?> UpdateFormatTypeAsync(FormatTypeUpdateRequest formatType, int id);
        Task<FormatTypeResponse?> DeleteFormatTypeByIdAsync(int id);
    }
}
