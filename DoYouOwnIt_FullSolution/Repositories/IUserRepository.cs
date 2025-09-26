namespace DoYouOwnIt.Api.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUserByUserNameAsync(string UserName);
    }
}
