using DoYouOwnIt.Shared.Models.Format;
using DoYouOwnIt.Shared.Models.FormatType;

namespace DoYouOwnIt.Client.Services
{
    public interface IFormatTypeService
    {
        event Action? OnChange;
        List<FormatTypeResponse> FormatTypes { get; }
        Task<FormatTypeResponse?> GetFormatTypeByID(int formatTypeId);
        Task<List<FormatTypeResponse>> GetFormatTypesByCategoryId(int categoryId);
        Task CreateFormatType(FormatTypeRequest formatType);
        Task UpdateFormatType(int formatTypeID, FormatTypeRequest formatType);
        Task DeleteFormatType(int formatTypeID);
    }
}
