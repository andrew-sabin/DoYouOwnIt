
using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class FormatTypeService : IFormatTypeService
    {
        readonly IFormatTypeRepository _formatTypeRepository;
        public FormatTypeService(IFormatTypeRepository formatTypeRepository)
        {
            _formatTypeRepository = formatTypeRepository;
        }
        public async Task<FormatTypeResponse> CreateFormatTypeAsync(FormatTypeCreateRequest formatType)
        {
            var newEntry = formatType.Adapt<FormatType>();
            var result = await _formatTypeRepository.CreateFormatTypeAsync(newEntry);
            return result.Adapt<FormatTypeResponse>();
        }

        public async Task<FormatTypeResponse?> DeleteFormatTypeByIdAsync(int id)
        {
            var result = await _formatTypeRepository.DeleteFormatTypeByIdAsync(id);
            if (result is null)
            {
                return null!;
            }
            return result.Adapt<FormatTypeResponse>();
        }

        public async Task<List<FormatTypeResponse>> GetAllFormatTypesAsync()
        {
            var result = await _formatTypeRepository.GetAllFormatTypesAsync();
            return result.Adapt<List<FormatTypeResponse>>();
        }

        public async Task<FormatTypeResponse?> GetFormatTypeByIdAsync(int id)
        {
            var result = await _formatTypeRepository.GetFormatTypeByIdAsync(id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatTypeResponse>();
        }

        public async Task<List<FormatTypeResponse>> GetFormatTypesByCategoryId(int categoryId)
        {
            var result = await _formatTypeRepository.GetFormatTypesByCategoryId(categoryId);

            return result.Adapt<List<FormatTypeResponse>>();
        }

        public async Task<FormatTypeResponse?> UpdateFormatTypeAsync(FormatTypeUpdateRequest formatType, int id)
        {
            var updatedEntry = formatType.Adapt<FormatType>();
            var result = await _formatTypeRepository.UpdateFormatTypeAsync(updatedEntry, id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatTypeResponse>();
        }
    }
}
