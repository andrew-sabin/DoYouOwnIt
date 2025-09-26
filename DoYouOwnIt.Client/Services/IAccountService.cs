namespace DoYouOwnIt.Client.Services
{
    public interface IAccountService
    {
        Task<bool> IsUserInRole(string userName, string roleName);
    }
}
