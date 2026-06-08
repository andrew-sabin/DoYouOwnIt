using DoYouOwnIt_Shared.Models.NewsBlog;
using DoYouOwnIt_Shared.Models.NewsUpdate;

namespace DoYouOwnIt.Client.Services
{
    public interface INewsBlogService
    {
        event Action? OnChange;
        List<NewsBlogResponse> NewsBlogs { get; set; }
        Task<NewsBlogResponse?> GetArticleBySlug(string slug);
        Task<NewsBlogResponse?> GetFirstTypeArticle(string type);
        Task<List<NewsBlogResponse>> GetLatestUpdates(string type, int amount, bool sticky);
        Task<List<NewsBlogResponse>> GetLatestStickies(int amount, bool sticky);
        Task CreateArticle (NewsBlogRequest request);
        Task UpdateArticle (int id, NewsBlogRequest article);
        Task DeleteArticle (int id);
    }
}
