using DoYouOwnIt.Shared.Entities.Extras;
using DoYouOwnIt.Shared.Entities.Interfaces;
using DoYouOwnIt.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Format : BaseEntity
    {
        public string? CoverImageUrl { get; set; } = string.Empty;
        public required string Type { get; set; } = string.Empty;
        public string? Edition { get; set; } = string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string Slug { get; set; } = string.Empty;
        public DateOnly? ReleaseDate { get; set; } = null;
        public bool IsLocked { get; set; } = false; // Can Only be edited by Admins and Moderators
        public bool IsAIAssisted { get; set; } = false;
        public string? AIAssistsWith { get; set; } = string.Empty; // What the AI assists with, if applicable
        public string? Description { get; set; } = string.Empty;
        public required OwnershipLevel OwnershipLevel { get; set; }
        public string DisplayVideoUrl { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? CreatorId { get; set; } // Guid of the user who created this format entry
        public string? ModifierId { get; set; } // Guid of the user who last modified this format entry
        public List<string?> ContributerIds { get; set; } = new List<string?>(); // List of Guids of users who contributed to this format entry
        public DateTime LastModified { get; set; }
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
        public List<Image> DisplayImages { get; set; } = new List<Image>();

        

        /* 
         * TODO: 
         * 1.) Add properties for availability and price when availability entity is implemented
         *  Add all availbility prices, divide by all availabilities, and then set the average price here
         *  public decimal AveragePrice { get; set; } = 0.0m;
         */
    }
}
