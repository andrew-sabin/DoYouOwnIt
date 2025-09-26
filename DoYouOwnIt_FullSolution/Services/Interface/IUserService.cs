using DoYouOwnIt.Shared.Models.User;

namespace DoYouOwnIt.Api.Services.Interface
{
    public interface IUserService
    {
        Task<UserResponse?> GetAsync(string UserName);
        Task<UserResponse?> UpdateUser(string userName, UserUpdateRequest userRequest);
        Task<UserResponse?> AdminUpdateUser(string userName, AdminUserUpdateRequest userRequest);
        Task<UserResponse?> GetUserById(string UserId);
    }
}
