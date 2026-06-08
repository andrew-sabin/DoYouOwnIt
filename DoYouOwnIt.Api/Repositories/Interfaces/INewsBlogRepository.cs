using DoYouOwnIt_Shared.Entities;
using DoYouOwnIt_Shared.Models.NewsBlog;
using DoYouOwnIt_Shared.Models.NewsUpdate;

namespace DoYouOwnIt.Api.Services.Interface
{
    public interface INewsBlogRepository
    {
        Task<List<NewsBlog>> GetAllNewsBlogs();
        Task<NewsBlog?> GetNewsBlogByIdAsync(int id);
        Task<NewsBlog?> GetNewsBlogBySlugAsync(string slug);
        Task<NewsBlog?> GetFirstBlogByType(string type);
        Task<List<NewsBlog>> GetLastestBlogsByType(string type, int amnt, bool sticky);
        Task<List<NewsBlog>> GetLatestStickyBlogs(int amnt, bool sticky);
        Task<NewsBlog> CreateNewsBlogAsync(NewsBlog article);
        Task<NewsBlog?> UpdateNewsBlogAsync(int id, NewsBlog article);
        Task<List<NewsBlog>?> DeleteNewsBlogAsync(int id);

    }
}
