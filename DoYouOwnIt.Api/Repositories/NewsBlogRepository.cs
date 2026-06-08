using DoYouOwnIt_Shared.Entities;
using DoYouOwnIt_Shared.Models.NewsBlog;
using DoYouOwnIt_Shared.Models.NewsUpdate;

namespace DoYouOwnIt.Api.Repositories
{
    public class NewsBlogRepository : INewsBlogRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public NewsBlogRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<NewsBlog> CreateNewsBlogAsync(NewsBlog article)
        {
            article.AuthorId = _userContextService.GetUserId();
            article.CreatedDate = DateTime.UtcNow;
            article.ModifierId = _userContextService.GetUserId();
            article.ModifiedDate = article.CreatedDate;
            article.Slug = SlugHelper.GenerateNewBlogSlug(article.Title, article.CreatedDate);
            _context.NewsBlogs.Add(article);
            await _context.SaveChangesAsync();
            return article;

        }

        public async Task<List<NewsBlog>?> DeleteNewsBlogAsync(int id)
        {
            var dbArticle = await _context.NewsBlogs.FirstOrDefaultAsync(a => a.Id == id);
            if (dbArticle == null || dbArticle!.IsDeleted == true)
                return null;

            dbArticle.IsDeleted = true;
            dbArticle.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetAllNewsBlogs();
        }

        public async Task<List<NewsBlog>> GetAllNewsBlogs()
        {
            return await _context.NewsBlogs
                .Where(f => f.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<NewsBlog?> GetFirstBlogByType(string type)
        {
            var article = await _context.NewsBlogs.FirstOrDefaultAsync(a => a.ArticleType == type);
            return article;
        }

        public async Task<List<NewsBlog>> GetLastestBlogsByType(string type, int amnt, bool sticky)
        {
            if (sticky)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    var article = await _context.NewsBlogs.OrderByDescending(a => a.CreatedDate)
                    .Where(a => a.IsDeleted != true && a.ArticleType == type && a.StickToFrontPage)
                    .Take(amnt)
                    .ToListAsync();
                    return article;
                }
                else
                {
                    var article = await _context.NewsBlogs.OrderByDescending(a => a.CreatedDate)
                    .Where(a => a.IsDeleted != true && a.StickToFrontPage)
                    .Take(amnt)
                    .ToListAsync();
                    return article;
                }
            }
            else
            {
                var article = await _context.NewsBlogs.OrderByDescending(a => a.CreatedDate)
                .Where(a => a.IsDeleted != true && a.ArticleType == type)
                .Take(amnt)
                .ToListAsync();
                return article;
            }
        }

        public async Task<List<NewsBlog>> GetLatestStickyBlogs(int amnt, bool sticky)
        {
            var article = await _context.NewsBlogs.OrderByDescending(a => a.CreatedDate)
                .Where(a => a.IsDeleted != true && a.StickToFrontPage)
                .Take(amnt)
                .ToListAsync();
            return article;
        }

        public async Task<NewsBlog?> GetNewsBlogByIdAsync(int id)
        {
            var article = await _context.NewsBlogs
                .FirstOrDefaultAsync(a => a.Id == id);
            return article;
        }

        public async Task<NewsBlog?> GetNewsBlogBySlugAsync(string slug)
        {
            var article = await _context.NewsBlogs
                .FirstOrDefaultAsync(a => a.Slug == slug);
            return article;
        }

        public async Task<NewsBlog?> UpdateNewsBlogAsync(int id, NewsBlog article)
        {
            var dbArticle = await _context.NewsBlogs.FirstOrDefaultAsync(a => a.Id == id);
            if (dbArticle == null)
                return null;

            var modifierId = _userContextService.GetUserId();
            article.ModifiedDate = DateTime.UtcNow;

            dbArticle.ModifiedDate = article.ModifiedDate;
            dbArticle.ModifierId = modifierId;

            dbArticle.Title = article.Title;
            dbArticle.CoverImageUrl = article.CoverImageUrl;
            dbArticle.StickToFrontPage = article.StickToFrontPage;
            dbArticle.Slug = article.Slug;
            dbArticle.ArticleType = article.ArticleType;
            dbArticle.NewsArticle = article.NewsArticle;

            await _context.SaveChangesAsync();
            return await GetNewsBlogByIdAsync(id);
        }
    }
}
