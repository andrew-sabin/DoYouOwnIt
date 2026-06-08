using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Product
{
    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Creators { get; set; } = string.Empty;
        public string? CoverImageURL { get; set; }
        public int ContentRating { get; set; } = 3;
        public string? Slug { get; set; } = string.Empty; // Slug for SEO and URL purposes, should be unique
        public DateOnly ProductLaunchDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsLocked { get; set; } = false;
        public string? LockedBy { get; set; } = string.Empty;
        public string? lockedReason {  get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DescriptionSource { get; set; }
        public bool IsAIAssisted { get; set; }
        public string? AIAssistsWith { get; set; }
        public bool ForMatureAudiences { get; set; }
        public string? MatureAudienceReason { get; set; }
        public bool HasIssue { get; set; }
        [MaxLength(150)]
        public string? Issue { get; set; }
        [MaxLength(100)]
        public string? IssueURL { get; set; }
        public string? CreatorId { get; set; }
        public string? ModifierId { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        [Range(1, int.MaxValue, ErrorMessage ="Please select a Category.")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
