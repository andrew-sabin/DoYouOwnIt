using DoYouOwnIt_Shared.Entities;
using DoYouOwnIt_Shared.Models.NewsBlog;
using DoYouOwnIt_Shared.Models.NewsUpdate;
using Mapster;

namespace DoYouOwnIt.Api.Services
{
    public class NewsBlogService : INewsBlogService
    {
        private readonly INewsBlogRepository _newsBlogRepository;

        public NewsBlogService(INewsBlogRepository newsBlogRepository)
        {
            _newsBlogRepository = newsBlogRepository;
        }

        public async Task<NewsBlogResponse> CreateNewsBlogAsync(NewsBlogCreateRequest article)
        {
            var newArticle = article.Adapt<NewsBlog>();
            var result = await _newsBlogRepository.CreateNewsBlogAsync(newArticle);
            return result.Adapt<NewsBlogResponse>();
        }

        public async Task<List<NewsBlogResponse>?> DeleteFormatAsync(int id)
        {
            var result = await _newsBlogRepository.DeleteNewsBlogAsync(id);
            if(result == null)
                return null;
            return result.Adapt<List<NewsBlogResponse>>();
        }

        public async Task<List<NewsBlogResponse>> GetAllNewsBlogs()
        {
            var result = await _newsBlogRepository.GetAllNewsBlogs();
            return result.Adapt<List<NewsBlogResponse>>();
        }

        public async Task<NewsBlogResponse?> GetFirstBlogByType(string type)
        {
            var result = await _newsBlogRepository.GetFirstBlogByType(type);
            return result.Adapt<NewsBlogResponse>();
        }

        public async Task<List<NewsBlogResponse>?> GetLastestBlogsByType(string type, int amnt, bool sticky)
        {
            var result = await _newsBlogRepository.GetLastestBlogsByType(type, amnt, sticky);
            return result.Adapt<List<NewsBlogResponse>>();
        }

        public async Task<List<NewsBlogResponse>?> GetLatestStickyBlogs(int amnt, bool sticky)
        {
            var result = await _newsBlogRepository.GetLatestStickyBlogs(amnt, sticky);
            return result.Adapt<List<NewsBlogResponse>>();
        }

        public async Task<NewsBlogResponse?> GetNewsBlogByIdAsync(int id)
        {
            var result = await _newsBlogRepository.GetNewsBlogByIdAsync(id);
            if (result == null)
                return null;
            return result.Adapt<NewsBlogResponse>();
        }

        public async Task<NewsBlogResponse?> GetNewsBlogBySlugAsync(string slug)
        {
            var result = await _newsBlogRepository.GetNewsBlogBySlugAsync(slug);
            if (result == null)
                return null;
            return result.Adapt<NewsBlogResponse>();
        }

        public async Task<NewsBlogResponse?> UpdateNewsBlogAsync(int id, NewsBlogUpdateRequest article)
        {
            var updatedEntry = article.Adapt<NewsBlog>();
            var result = await _newsBlogRepository.UpdateNewsBlogAsync(id, updatedEntry);
            if (result == null)
                return null;
            return result.Adapt<NewsBlogResponse>();

        }
    }
}
