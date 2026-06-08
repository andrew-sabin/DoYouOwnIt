namespace DoYouOwnIt.Api.Services
{
    public interface IUserContextService
    {
        string? GetUserId();
        string? GetUserName();
        Task<ApplicationUser?> GetUserAsync();
    }
}
