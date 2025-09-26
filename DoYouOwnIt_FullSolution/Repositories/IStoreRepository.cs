namespace DoYouOwnIt.Api.Repositories
{
    public interface IStoreRepository
    {
        Task <List<Store>> GetAllStoresAsync();
        Task <Store?> GetStoreByIdAsync(int id);
        Task <Store?> GetStoreBySlugAsync(string slug);
        Task <List<Store>> CreateStoreAsync(Store store);
        Task <Store?> UpdateStoreAsync(int id, Store store);
        Task<List<Store>?> DeleteStoreAsync(int id);
    }
}
