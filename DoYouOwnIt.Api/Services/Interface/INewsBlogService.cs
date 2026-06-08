using DoYouOwnIt_Shared.Entities;
using DoYouOwnIt_Shared.Models.NewsBlog;
using DoYouOwnIt_Shared.Models.NewsUpdate;

namespace DoYouOwnIt.Api.Services.Interface
{
    public interface INewsBlogService
    {
        Task<List<NewsBlogResponse>> GetAllNewsBlogs();
        Task<NewsBlogResponse?> GetNewsBlogByIdAsync(int id);
        Task<NewsBlogResponse?> GetNewsBlogBySlugAsync(string slug);
        Task<NewsBlogResponse?> GetFirstBlogByType(string type);
        Task<List<NewsBlogResponse>?> GetLastestBlogsByType(string type, int amnt, bool sticky);
        Task<List<NewsBlogResponse>?> GetLatestStickyBlogs(int amnt, bool sticky);
        Task<NewsBlogResponse> CreateNewsBlogAsync(NewsBlogCreateRequest article);
        Task<NewsBlogResponse?> UpdateNewsBlogAsync(int id, NewsBlogUpdateRequest article);
        Task<List<NewsBlogResponse>?> DeleteFormatAsync(int id);

    }
}
