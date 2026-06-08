using DoYouOwnIt.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DoYouOwnIt_Shared.Entities
{
    public class NewsBlog : SoftDeleteableEntity
    {
        [Required]
        public string Title {  get; set; } = string.Empty;
        public string ArticleType { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; } = string.Empty;
        public string Slug { get; set; }= string.Empty;
        public bool StickToFrontPage { get; set; } = false;
        public string? AuthorId { get; set; } = string.Empty;
        public string? ModifierId { get; set; } // Guid of the user who last modified this format entry
        [Required]
        public string NewsArticle { get; set; } = string.Empty;
    }
}
