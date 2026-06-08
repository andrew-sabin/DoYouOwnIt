using DoYouOwnIt.Shared.Enums;
using DoYouOwnIt.Shared.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using DoYouOwnIt.Shared.Models.FormatType;

namespace DoYouOwnIt.Shared.Models.Format
{
    public record struct FormatResponse
    {
        public int Id { get; init; }
        public string CoverImageUrl { get; init; }
        public DateOnly? ReleaseDate { get; init; }
        public string? Slug { get; init; }
        public string Type { get; init; }
        public int FormatTypeId { get; init; }
        public string Edition { get; init; }
        public string? ModifierName { get; init; }
        public DateTime ModifiedDate { get; init; }
        public string? ModifierId { get; init; }
        public bool IsLocked { get; init; }
        public string? lockedReason { get; init; }
        public string? LockedByUser { get; init; }
        public string? CreatorId { get; init; }
        public ProductResponseJustCategoryId? Product { get; init; }
        public FormatTypeResponseNoCategory FormatType { get; init; }
        public int formatrevisionid { get; init; }

    }
}

