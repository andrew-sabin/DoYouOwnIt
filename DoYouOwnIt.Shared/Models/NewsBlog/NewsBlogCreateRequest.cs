using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.NewsUpdate
{
    public record struct NewsBlogCreateRequest
    {
        public string Title { get; init; }
        public string? CoverImageUrl { get; init; }
        public bool StickToFrontPage { get; set; }
        public string ArticleType { get; init; }
        public string NewsArticle { get; init; }
    }
}
