
using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class Format_Service : IFormatService
    {
        private readonly IFormatRepository _formatRepository;

        public Format_Service(IFormatRepository formatRepository)
        {
            _formatRepository = formatRepository;
        }

        public async Task<FormatResponse> CreateFormatAsync(FormatCreateRequest format)
        {
            var newEntry = format.Adapt<Format>();
            var result = await _formatRepository.CreateFormatAsync(newEntry);
            return result.Adapt<FormatResponse>();
        }

        public async Task<List<FormatResponse>?> DeleteFormatAsync(int id)
        {
            var result = await _formatRepository.DeleteFormatAsync(id);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<FormatResponse>>();
        }

        public async Task<List<FormatResponse>> GetAllFormatsAsync()
        {
            var result = await _formatRepository.GetAllFormatsAsync();
            return result.Adapt<List<FormatResponse>>();
        }

        public async Task<FormatResponse?> GetFormatByIdAdminAsync(int id)
        {
            var result = await _formatRepository.GetFormatByIdAdmin(id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatResponse>();
        }

        public async Task<FormatResponse?> GetFormatByIdAsync(int id)
        {
            var result = await _formatRepository.GetFormatByIdAsync(id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatResponse>();
        }

        public async Task<FormatResponse?> GetFormatBySlugAdminAsync(string prodSlug, string slug)
        {
            var result = await _formatRepository.GetFormatBySlugAdminAsync(prodSlug, slug);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatResponse>();
        }

        public async Task<FormatResponse?> GetFormatBySlugAsync(string prodSlug, string slug)
        {
            var result = await _formatRepository.GetFormatBySlugAsync(prodSlug, slug);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatResponse>();
        }

        public async Task<List<FormatResponse>?> GetFormatsByProductIdAdminAsync(int productId)
        {
            var results = await _formatRepository.GetFormatByProductIdAdminAsync(productId);
            return results.Adapt<List<FormatResponse>>();
        }

        public async Task<List<FormatResponse>?> GetFormatsByProductIdAsync(int productId)
        {
            var results = await _formatRepository.GetFormatsByProductIdAsync(productId);
            return results.Adapt<List<FormatResponse>>();
        }

        public async Task<List<FormatResponse>> GetRecentFormatsAsync(int amount)
        {
            var result = await _formatRepository.GetRecentFormats(amount);
            return result.Adapt<List<FormatResponse>>();
        }

        public async Task<List<FormatResponse>> GetRecentlyUpdatedFormatsAsync(int amount)
        {
            var result = await _formatRepository.GetRecentlyUpdatedFormats(amount);
            return result.Adapt<List<FormatResponse>>();
        }

        public async Task<FormatResponse?> LockFormatAsync(int id, FormatLockRequest format)
        {
            var lockedentry = format.Adapt<Format>();
            var result = await _formatRepository.LockFormatAsync(id, lockedentry);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<FormatResponse>();
        }

        public async Task<FormatResponse?> UpdateFormatAsync(int id, FormatUpdateRequest format)
        {
            var updatedEntry = format.Adapt<Format>();
            var result = await _formatRepository.UpdateFormatAsync(id, updatedEntry);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<FormatResponse>();
        }
    }
}
