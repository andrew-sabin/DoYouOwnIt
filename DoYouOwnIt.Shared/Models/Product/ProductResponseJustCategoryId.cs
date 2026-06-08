using DoYouOwnIt.Shared.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Product
{
    public class ProductResponseJustCategoryId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CoverImageURL { get; set; }
        public DateOnly ProductLaunchDate { get; set; }
        public string? Description { get; set; }
        public string? DescriptionSource { get; set; }
        public string? Slug { get; set; }
        public int CategoryId { get; set; }
        public bool IsLocked { get; set; }
        public string? Creators { get; set; }
        public int ContentRating {  get; set; }
        public bool IsAIAssisted { get; set; }
        public bool ForMatureAudiences { get; set; }
        public string? MatureAudienceReason { get; set; }
        public bool HasIssue { get; set; }
        [MaxLength(150)]
        public string? Issue { get; set; }
        [MaxLength(100)]
        public string? IssueURL { get; set; }
        public string? CreatorId { get; set; }
        public string? CategoryName { get; set; }
        public string? ModifierId  { get; set; }
        public DateTime ModifiedDate { get; set; }
    };
        
}
