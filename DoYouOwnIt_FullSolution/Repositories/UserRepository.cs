using Microsoft.AspNetCore.Identity;

namespace DoYouOwnIt.Api.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ApplicationUser?> GetUserByUserNameAsync(string UserName)
        {
            return await _userManager.FindByNameAsync(UserName);
        }
    }
}
