using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.NewsBlog
{
    public record struct NewsBlogUpdateRequest
    {
        public string Title { get; init; }
        public string Slug { get; init; }
        public string CoverImageUrl { get; init; }
        public bool StickToFrontPage { get; init; }
        public string ArticleType { get; init; }
        public string NewsArticle { get; init; }
    }
}
