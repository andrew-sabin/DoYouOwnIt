using DoYouOwnIt.Shared.Models.Store;

namespace DoYouOwnIt.Client.Services
{
    public interface IStoreService
    {
        event Action? OnChange;
        List<StoreResponse> Stores { get; set; }
        Task LoadAllStoresAsync();
        Task <StoreResponse?> GetStoreByIdAsync(int id);
        Task <StoreResponse?> GetStoreBySlugAsync(string slug);
        Task CreateStoreAsync(StoreRequest store);
        Task UpdateStoreAsync(int id, StoreRequest store);
        Task DeleteStoreAsync(int id);
    }
}
