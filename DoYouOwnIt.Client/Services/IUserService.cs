using DoYouOwnIt.Shared.Models.User;
using DoYouOwnIt_Shared.Models.User;

namespace DoYouOwnIt.Client.Services
{
    public interface IUserService
    {
        event Action? OnChange;
        Task <UserResponse?> GetUserAsync(string userName);
        Task UpdateUserAsync(string userName, UserRequest userRequest);
        Task UpdateUserAdminAsync(string userName, UserRequest userRequest);
        Task <bool> IsUserInRole(string userName, string roleName);
        Task <UserResponse?> GetUserByIdAsync(string userId);
        Task UpdateUserRole(string userName, UserRoleAddRequest rolAdd , UserRemoveRoleRequest roleRemove);
        Task<IList<string>> GetUserRole(string userName);
    }
}
