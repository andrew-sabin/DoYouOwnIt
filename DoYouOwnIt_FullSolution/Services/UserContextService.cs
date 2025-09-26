using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DoYouOwnIt.Api.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext?.User;
            if (httpContext == null)
            {
                return null;
            }

            return await _userManager.GetUserAsync(httpContext);
        }

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
