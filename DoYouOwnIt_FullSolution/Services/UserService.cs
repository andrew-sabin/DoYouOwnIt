using DoYouOwnIt.Api.Services.Interface;
using DoYouOwnIt.Shared.Models.User;
using Microsoft.AspNetCore.Identity;

namespace DoYouOwnIt.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserContextService _userContextService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUserContextService userContextService, 
            UserManager<ApplicationUser> userManager)
        {
            _userContextService = userContextService;
            _userManager = userManager;
        }

        public async Task<UserResponse?> AdminUpdateUser(string userName, AdminUserUpdateRequest userRequest)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            user.DisplayName = userRequest.DisplayName!;
            user.UserName = userRequest.UserName;
            user.ProfileImageURL = userRequest.ProfileImageURL;
            user.WebsiteURL = userRequest.WebsiteURL;
            user.Bio = userRequest.Bio;
            user.IsVerified = userRequest.IsVerified;
            user.IsBanned = userRequest.IsBanned;
            user.BanEndDate = userRequest.BanEndDate;
            user.BanReason = userRequest.BanReason;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var response = new UserResponse
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    DisplayName = user?.DisplayName,
                    ProfileImageURL = user?.ProfileImageURL,
                    WebsiteURL = user?.WebsiteURL,
                    Bio = user?.Bio,
                    IsVerified = user!.IsVerified,
                    IsBanned = user.IsBanned,
                    BanEndDate = user.BanEndDate,
                    BanReason = userRequest.BanReason,
                    UpdatedAt = user?.UpdatedAt ?? DateTime.MinValue
                };
                return response;
            }
            return null;
        }

        public async Task<UserResponse?> GetAsync(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return null;
            }
            else
            {
                var response = new UserResponse
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    DisplayName = user?.DisplayName,
                    ProfileImageURL = user?.ProfileImageURL,
                    WebsiteURL = user?.WebsiteURL,
                    Bio = user?.Bio,
                    IsVerified = user?.IsVerified ?? false,
                    CreatedAt = user?.CreatedAt ?? DateTime.MinValue,
                    UpdatedAt = user?.UpdatedAt ?? DateTime.MinValue,
                    IsBanned = user?.IsBanned ?? false,
                    BanEndDate = user?.BanEndDate,
                    BanReason = user?.BanReason
                };
                return response;
            }
        }

        public async Task<UserResponse?> GetUserById(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                return new UserResponse
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    DisplayName = user?.DisplayName,
                    ProfileImageURL = user?.ProfileImageURL,
                    WebsiteURL = user?.WebsiteURL,
                    Bio = user?.Bio,
                    IsVerified = user?.IsVerified ?? false,
                    CreatedAt = user?.CreatedAt ?? DateTime.MinValue,
                    UpdatedAt = user?.UpdatedAt ?? DateTime.MinValue,
                    IsBanned = user?.IsBanned ?? false,
                    BanEndDate = user?.BanEndDate,
                    BanReason = user?.BanReason
                };
            }
            return null;
        }

        public async Task<UserResponse?> UpdateUser(string userName, UserUpdateRequest userRequest)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }
            user.DisplayName = userRequest.DisplayName;
            user.ProfileImageURL = userRequest.ProfileImageURL;
            user.WebsiteURL = userRequest.WebsiteURL;
            user.Bio = userRequest.Bio;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var response = new UserResponse
                {
                    UserName = user?.UserName,
                    Email = user?.Email,
                    DisplayName = user?.DisplayName,
                    WebsiteURL = user?.WebsiteURL,
                    ProfileImageURL = user?.ProfileImageURL,
                    Bio = user?.Bio,
                    UpdatedAt = user?.UpdatedAt ?? DateTime.MinValue,
                };
                return response;
            }
            return null;
        }
    }
}
