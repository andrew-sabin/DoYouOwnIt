namespace DoYouOwnIt.Api.Services
{
    public interface IAvailabilityService
    {
        Task<List<AvailabilityResponse>> GetAllAvailabilitiesAsync();
        Task<AvailabilityResponse?> GetAvailabilityByIdAsync(int id);
        Task<List<AvailabilityResponse>?> GetAvailabilitiesByFormatIdAsync(int formatId);
        Task<List<AvailabilityResponse>?> GetAvailabilitiesByStoreIdAsync(int storeId);
        Task<List<AvailabilityResponse>> CreateAvailabilityAsync(AvailabilityCreateRequest availability);
        Task<AvailabilityResponse?> UpdateAvailabilityAsync(int id, AvailabilityUpdateRequest availability);
        Task<List<AvailabilityResponse>?> DeleteAvailabilityAsync(int id);
    }
}
