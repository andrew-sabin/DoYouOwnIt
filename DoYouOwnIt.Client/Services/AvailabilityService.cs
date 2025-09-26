using DoYouOwnIt.Shared.Models.Availability;
using System.Net.Http;
using System.Net.Http.Json;

namespace DoYouOwnIt.Client.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly HttpClient _httpClient;

        public AvailabilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<AvailabilityResponse> Availabilities { get; set; }

        public event Action? OnChange;

        public async Task CreateAvailability(AvailabilityRequest availability)
        {
            var createRequest = new AvailabilityCreateRequest
            {
                FormatId = availability.FormatId,
                StoreId = availability.StoreId,
                URL = availability.URL,
                CurrencyCode = availability.CurrenceyCode,
                UnitSold = availability.UnitSold,
                Price = availability.Price
            };
            Console.WriteLine(createRequest);
            await _httpClient.PostAsJsonAsync("api/availability", createRequest);
        }

        public async Task DeleteAvailability(int availabilityID)
        {
            await _httpClient.DeleteAsync($"api/availability/{availabilityID}");
        }

        public async Task GetAvailabilitiesByFormatId(int formatId)
        {
            List<AvailabilityResponse>? result = null;
            if (formatId <= 0)
            {
                result = await _httpClient.GetFromJsonAsync<List<AvailabilityResponse>>("api/availability");
            }
            else
            {
                result = await _httpClient.GetFromJsonAsync<List<AvailabilityResponse>>($"api/availability/format/{formatId}");
            }

            if (result is not null)
            {
                Availabilities = result;
                OnChange?.Invoke();
            }
            else
            {
                Availabilities = new List<AvailabilityResponse>();
            }
        }

        public async Task<AvailabilityResponse?> GetAvailabilityByID(int availId)
        {
            return await _httpClient.GetFromJsonAsync<AvailabilityResponse>($"api/Availability/{availId}");
        }

        public Task UpdateAvailability(int availabilityID, AvailabilityRequest availability)
        {
            var updateRequest = new AvailabilityUpdateRequest
            {
                FormatId = availability.FormatId,
                StoreId = availability.StoreId,
                URL = availability.URL,
                CurrencyCode = availability.CurrenceyCode,
                UnitSold = availability.UnitSold,
                Price = availability.Price
            };
            Console.WriteLine($"Updating Availability:{availabilityID}");
            return _httpClient.PutAsJsonAsync($"api/availability/{availabilityID}", updateRequest);
        }
    }
}
