namespace DoYouOwnIt.Api.Services
{
    public interface IStoreService
    {
        Task<List<StoreResponse>> GetAllStoresAsync();
        Task<StoreResponse?> GetStoreByIdAsync(int id);
        Task<StoreResponse?> GetStoreBySlugAsync(string slug);
        Task<List<StoreResponse>> CreateStoreAsync(StoreCreateRequest store);
        Task<StoreResponse?> UpdateStoreAsync(int id, StoreUpdateRequest store);
        Task<StoreResponse?> LockStoreAsync(int id, StoreLockRequest lockRequest);
        Task<List<StoreResponse>?> DeleteStoreAsync(int id);
    }
}
