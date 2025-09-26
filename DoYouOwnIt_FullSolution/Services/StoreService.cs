using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<List<StoreResponse>> CreateStoreAsync(StoreCreateRequest store)
        {
            var newEntry = store.Adapt<Store>();
            var result = await _storeRepository.CreateStoreAsync(newEntry);
            return result.Adapt<List<StoreResponse>>();
        }

        public async Task <List<StoreResponse>?> DeleteStoreAsync(int id)
        {
            var result = await _storeRepository.DeleteStoreAsync(id);
            if (result == null)
                return null;

            return result.Adapt<List<StoreResponse>>();
        }

        public async Task<List<StoreResponse>> GetAllStoresAsync()
        {
            var result = await _storeRepository.GetAllStoresAsync();
            return result.Adapt<List<StoreResponse>>();
        }

        public async Task<StoreResponse?> GetStoreByIdAsync(int id)
        {
            var result = await _storeRepository.GetStoreByIdAsync(id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<StoreResponse>();
        }

        public async Task<StoreResponse?> GetStoreBySlugAsync(string slug)
        {
            var result = await _storeRepository.GetStoreBySlugAsync(slug);
            return result.Adapt<StoreResponse?>();
        }

        public async Task<StoreResponse?> UpdateStoreAsync(int id, StoreUpdateRequest store)
        {
            var updatedStore = store.Adapt<Store>();
            var result = await _storeRepository.UpdateStoreAsync(id, updatedStore);
            if (result == null)
                return null;

            return result.Adapt<StoreResponse>();
        }
    }
}
