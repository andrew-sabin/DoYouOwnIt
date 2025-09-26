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
    public record struct FormatResponse
    {
        public int Id { get; init; }
        public string Type { get; init; }
        public string Edition { get; init; }
        public string CoverImageUrl { get; init; }
        public string DisplayVideoURL { get; init; }
        public DateOnly? ReleaseDate { get; init; }
        public string? Slug { get; init; }
        public string Description { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OwnershipLevel OwnershipLevel { get; init; }
        public bool IsAIAssisted { get; init; }
        public string? AIAssistsWith {  get; init; }
        public bool IsLocked { get; init; }
        public string? CreatorId { get; init; }
        public string? ModifierId { get; init; }
        public DateTime LastModified { get; init; }
        public List<string?> ContributerIds { get; init; }
        public ProductResponseNoCategory Product { get; init; }
    }
}

