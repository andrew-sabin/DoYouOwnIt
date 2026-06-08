using DoYouOwnIt.Shared.Entities;
using DoYouOwnIt.Shared.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Entities
{
    public class FormatType : BaseEntity
    {
        [StringLength(50, ErrorMessage = "FormatType Name cannot exceed 50 characters.")]
        public required string Name { get; set; }
        public string? ImageUrl { get; set; } = String.Empty;
        public bool HasIssue { get; set; }
        [MaxLength(150)]
        public string? Issue { get; set; }
        [MaxLength(100)]
        public string? IssueURL { get; set; }

        [StringLength(150, ErrorMessage = "FormatType Description cannot exceed 150 characters.")]
        public string Description { get; set; } = String.Empty;
        public int CategoryId {  get; set; }
        public required Category Category { get; set; }
        public List<Format>? FormatRevisions { get; set; }
    }
}
