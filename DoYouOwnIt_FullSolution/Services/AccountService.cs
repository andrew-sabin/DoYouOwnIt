using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Account
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            await _userManager.AddToRoleAsync(user!, roleName);
        }

        public async Task<bool> IsUserInRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return await _userManager.IsInRoleAsync(user!, roleName);
        }

        public Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request)
        {
            var newUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
            };
            var result = _userManager.CreateAsync(newUser, request.Password);

            if (result.Result.Succeeded)
            {
                return Task.FromResult(new AccountRegistrationResponse
                {
                    Success = true
                });
            }
            else
            {
                var errors = result.Result.Errors.Select(e => e.Description).ToList();
                return Task.FromResult(new AccountRegistrationResponse
                {
                    Success = false,
                    Errors = errors
                });
            }
        }

        public async Task RemoveRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            await _userManager.RemoveFromRoleAsync(user!, roleName);
        }
    }
}
