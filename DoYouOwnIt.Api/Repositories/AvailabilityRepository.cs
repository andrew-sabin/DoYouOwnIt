
namespace DoYouOwnIt.Api.Repositories
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public AvailabilityRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<Availability>> CreateAvailabilityAsync(Availability availability)
        {
            availability.LastCheckedBy = _userContextService.GetUserId();
            _context.Add(availability);
            await _context.SaveChangesAsync();
            return await _context.Availabilities.ToListAsync();
        }

        public async Task<List<Availability>?> DeleteAvailabilityAsync(int id)
        {
            var dbAvailability = _context.Availabilities.FirstOrDefault(a => a.Id == id);
            if (dbAvailability == null)
            {
                return null;
            }
            var formatId = dbAvailability.FormatId;

            _context.Availabilities.Remove(dbAvailability);
            await _context.SaveChangesAsync();

            return await GetAvailabilitiesByFormatIdAsync(formatId);

        }

        public async Task<List<Availability>> GetAllAvailabilitiesAsync()
        {
            return await _context.Availabilities
                .Include(a => a.Store)
                .Include(a => a.Format)
                .ToListAsync();
        }

        public async Task<List<Availability>> GetAvailabilitiesByFormatIdAsync(int formatId)
        {
            return await _context.Availabilities
                .Include(a => a.Store)
                .Include(a => a.Format)
                .Where(a => a.FormatId == formatId)
                .ToListAsync();
        }

        public async Task<List<Availability>> GetAvailabilitiesByStoreIdAsync(int storeId)
        {
            return await _context.Availabilities
                .Include(a => a.Store)
                .Include(a => a.Format)
                .Where(a => a.StoreId == storeId)
                .ToListAsync();
        }

        public async Task<Availability?> GetAvailabilityByIdAsync(int id)
        {
            return await _context.Availabilities
                .Include(a => a.Store)
                .Include(a => a.Format)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Availability?> UpdateAvailabilityAsync(int id, Availability availability)
        {
            var dbAvailability = await _context.Availabilities.FirstOrDefaultAsync(a=>a.Id == id);
            if (dbAvailability == null)
            {
                return null;
            }
            availability.LastCheckedBy = _userContextService.GetUserId();
            dbAvailability.CurrencyCode = availability.CurrencyCode;
            dbAvailability.StoreId = availability.StoreId;
            dbAvailability.FormatId = availability.FormatId;
            dbAvailability.UnitSold = availability.UnitSold;
            dbAvailability.Price = availability.Price;
            dbAvailability.CurrencyCode = availability.CurrencyCode;
            dbAvailability.URL = availability.URL;
            dbAvailability.LastCheckedBy = availability.LastCheckedBy;

            dbAvailability.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetAvailabilityByIdAsync(id);
        }
    }
}
