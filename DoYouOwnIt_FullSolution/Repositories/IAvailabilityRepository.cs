namespace DoYouOwnIt.Api.Repositories
{
    public interface IAvailabilityRepository
    {
        Task<List<Availability>> GetAllAvailabilitiesAsync();
        Task<Availability?> GetAvailabilityByIdAsync(int id);
        Task<List<Availability>> CreateAvailabilityAsync(Availability availability);
        Task<List<Availability>> GetAvailabilitiesByFormatIdAsync(int formatId);
        Task<List<Availability>> GetAvailabilitiesByStoreIdAsync(int storeId);
        Task<Availability?> UpdateAvailabilityAsync(int id, Availability availability);
        Task<List<Availability>?> DeleteAvailabilityAsync(int id);
    }
}
