using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.NewsUpdate
{
    public class NewsBlogRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string ArticleType { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; } = string.Empty;
        public bool StickToFrontPage { get; set; } = false;
        public string AuthorId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string ModifierId { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; } 
        public string NewsArticle { get; set; } = string.Empty;
    }
}
