using DoYouOwnIt.Shared.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.FormatType
{
    public record struct FormatTypeResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? ImageUrl { get; init; }
        public string? Description { get; init; }
        public bool HasIssue { get; init; }
        [MaxLength(150)]
        public string? Issue { get; init; }
        [MaxLength(100)]
        public string? IssueURL { get; init; }
        public int CategoryId { get; init; }
        public CategoryResponse? Category { get; init; }
    }
}
