
using DoYouOwnIt.Shared.Entities;

namespace DoYouOwnIt.Api.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataContext _context;

        public StoreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Store>> CreateStoreAsync(Store store)
        {
            store.Slug = SlugHelper.GenerateSlug(store.Name);
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return await _context.Stores.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<List<Store>?> DeleteStoreAsync(int id)
        {
            var result = await _context.Stores.Where(s => !s.IsDeleted).FirstOrDefaultAsync(s => s.Id == id);
            if (result == null)
                return null;

            result.IsDeleted = true;
            result.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetAllStoresAsync();
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            return await _context.Stores.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<Store?> GetStoreByIdAsync(int id)
        {
            var store = await _context.Stores
                .Where(s => !s.IsDeleted)
                .FirstOrDefaultAsync(s => s.Id == id);
            return store;
        }

        public Task<Store?> GetStoreBySlugAsync(string slug)
        {
            var store = _context.Stores
                .FirstOrDefaultAsync(s => s.Slug == slug);
            return store;
        }

        public async Task<Store?> UpdateStoreAsync(int id, Store store)
        {
            var dbStore = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
            if (dbStore == null) {
                return null;
            }
            dbStore.Name = store.Name;
            dbStore.Industry = store.Industry;
            dbStore.LogoURL = store.LogoURL;
            dbStore.Online = store.Online;
            dbStore.Street = store.Street;
            dbStore.City = store.City;
            dbStore.State = store.State;
            dbStore.PostalCode = store.PostalCode;
            dbStore.Country = store.Country;
            dbStore.Phone = store.Phone;
            dbStore.Email = store.Email;

            if (string.IsNullOrEmpty(dbStore.Slug) && string.IsNullOrEmpty(store.Slug))
            {
                dbStore.Slug = SlugHelper.GenerateSlug(store.Name);
            }
            else if (!string.IsNullOrEmpty(store.Slug))
            {
                dbStore.Slug = store.Slug;
            }
            dbStore.WebsiteURL = store.WebsiteURL;

            dbStore.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetStoreByIdAsync(id);
        }
    }
}
