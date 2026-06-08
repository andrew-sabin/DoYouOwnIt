using DoYouOwnIt.Shared.Models.Roles;
using Microsoft.AspNetCore.Identity;

namespace DoYouOwnIt.Api.Services
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);
        Task DeleteRoleAsync(string roleName);
        Task<List<IdentityRole>> GetAllRolesAsync();
    }
}
