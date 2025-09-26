using DoYouOwnIt.Shared.Enums;
using DoYouOwnIt.Shared.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Format
{
    public record struct FormatUpdateRequest
    {
        public string Type { get; init; }
        public string Edition { get; init; }
        public string? Slug { get; init; }
        public string? CoverImageUrl { get; init; }
        public string? DisplayVideoURL { get; init; }
        public DateOnly? ReleaseDate { get; init; }
        public string Description { get; init; }
        public OwnershipLevel OwnershipLevel { get; init; }
        public bool IsAIAssisted { get; init; }
        public string? AIAssistsWith { get; init; }
        public int ProductId { get; init; }
    }
}
