namespace DoYouOwnIt.Api.Services
{
    public interface IUserContextService
    {
        string? GetUserId();
        Task<ApplicationUser?> GetUserAsync();
    }
}
