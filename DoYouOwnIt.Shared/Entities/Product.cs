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
        public required string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Slug { get; set; } = string.Empty; // Slug for SEO and URL purposes, should be unique
        public string? CoverImageURL { get; set; } = string.Empty;
        public DateOnly ProductLaunchDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsLocked { get; set; } = false; // Check to see if a product can only be edited by Admins and Moderators
        public string? Creators { get; set; } = string.Empty;
        public string? CreditsURL { get; set; } = string.Empty; // URL to the credits page or information, think IMDB/MovieDB/MobyGames/etc.
        public string? ContentRating { get; set; } = string.Empty;
        public bool IsAIAssisted { get; set; } = false;
        public string? AIAssistsWith { get; set; } = string.Empty; // What the AI assists with, if applicable
        public bool ForMatureAudiences { get; set; } = false;
        public string? MatureAudienceReason { get; set; } = string.Empty;
        [Column(TypeName = "TEXT")]
        [MaxLength(750)]
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
        public List<Format> Formats { get; set; } = new List<Format>();
        public string? CreatorId { get; set; } // Guid of the user who created this product entry
        public string? ModifierId { get; set; } // Guid of the user who last modified this product entry
        public DateTime LastModified { get; set; }


        // Override ToString for better debugging
        public override string ToString()
        {
            var formattedName = Name.Replace(" ", "_");

            return $"{formattedName}+({ProductLaunchDate.Year})";
        }




        
    }
}
