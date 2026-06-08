using DoYouOwnIt.Shared.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Product : SoftDeleteableEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Slug { get; set; } = string.Empty; // Slug for SEO and URL purposes, should be unique
        public string? CoverImageURL { get; set; } = string.Empty;
        public DateOnly ProductLaunchDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        [MaxLength(100)]
        public string? Creators { get; set; } = string.Empty;
        [Range(3,18)]
        public int ContentRating { get; set; } = 3;
        public bool IsAIAssisted { get; set; } = false;
        [MaxLength(150)]
        public string? AIAssistsWith { get; set; } = string.Empty; // What the AI assists with in creation, if applicable
        public bool ForMatureAudiences { get; set; } = false;
        [MaxLength(150)]
        public string? MatureAudienceReason { get; set; } = string.Empty;
        [Column(TypeName = "TEXT")]
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(150)]
        public string? DescriptionSource {  get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<Format> Formats { get; set; } = new List<Format>();
        public string? CreatorId { get; set; } // Guid of the user who created this product entry
        public string? ModifierId { get; set; } // Guid of the user who last modified this product entry
        
    }
}
