using DoYouOwnIt.Shared.Models.Availability;
using DoYouOwnIt.Shared.Models.Format;
namespace DoYouOwnIt.Client.Services
{
    public interface IAvailabilityService
    {
        event Action? OnChange;
        List<AvailabilityResponse> Availabilities { get; set; }
        Task GetAvailabilitiesByFormatId(int productId);
        Task<AvailabilityResponse?> GetAvailabilityByID(int availId);
        Task CreateAvailability(AvailabilityRequest availability);
        Task UpdateAvailability(int availabilityID, AvailabilityRequest availability);
        Task DeleteAvailability(int availabilityID);
    }
}
