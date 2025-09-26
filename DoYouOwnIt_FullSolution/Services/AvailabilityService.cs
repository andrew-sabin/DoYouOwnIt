
using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public AvailabilityService(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public async Task<List<AvailabilityResponse>> CreateAvailabilityAsync(AvailabilityCreateRequest availability)
        {
            var newAvailability = availability.Adapt<Availability>();
            var result = await _availabilityRepository.CreateAvailabilityAsync(newAvailability);
            return result.Adapt<List<AvailabilityResponse>>();
        }

        public async Task<List<AvailabilityResponse>> GetAllAvailabilitiesAsync()
        {
            var result = await _availabilityRepository.GetAllAvailabilitiesAsync();
            return result.Adapt<List<AvailabilityResponse>>();
        }

        public async Task<List<AvailabilityResponse>?> GetAvailabilitiesByFormatIdAsync(int formatId)
        {
            var result = await _availabilityRepository.GetAvailabilitiesByFormatIdAsync(formatId);
            return result.Adapt<List<AvailabilityResponse>>();
        }

        public async Task<AvailabilityResponse?> GetAvailabilityByIdAsync(int id)
        {
            var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
            if (availability == null)
            {
                return null;
            }
            return availability.Adapt<AvailabilityResponse>();
        }

        public async Task<List<AvailabilityResponse>?> GetAvailabilitiesByStoreIdAsync(int storeId)
        {
           var availabilities = await _availabilityRepository.GetAvailabilitiesByStoreIdAsync(storeId);
           if (availabilities == null)
           {
                return null;
           }
           return availabilities.Adapt<List<AvailabilityResponse>>();
        }

        public async Task<AvailabilityResponse?> UpdateAvailabilityAsync(int id, AvailabilityUpdateRequest availability)
        {
            var updatedAvailability = availability.Adapt<Availability>();
            var result = await _availabilityRepository.UpdateAvailabilityAsync(id, updatedAvailability);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<AvailabilityResponse>();
        }

        public async Task<List<AvailabilityResponse>?> DeleteAvailabilityAsync(int id)
        {
            var result = await _availabilityRepository.DeleteAvailabilityAsync(id);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<AvailabilityResponse>>();
        }
    }
}
