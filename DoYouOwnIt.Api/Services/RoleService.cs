
using DoYouOwnIt.Shared.Models.Roles;
using Microsoft.AspNetCore.Identity;

namespace DoYouOwnIt.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task CreateRoleAsync(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var roles = _roleManager.Roles.ToList();
            if (roles == null || !roles.Any())
            {
                return new List<IdentityRole>();
            }
            return roles;
        }
    }
}
