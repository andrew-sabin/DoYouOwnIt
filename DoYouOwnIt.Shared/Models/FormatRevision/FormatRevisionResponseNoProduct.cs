using DoYouOwnIt.Shared.Enums;
using DoYouOwnIt.Shared.Models.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.FormatRevision
{
    public record struct FormatRevisionResponseNoProduct
    {
        public int Id { get; init; }
        //public string? Type { get; init; }
        //public int FormatTypeId { get; init; }
        //public string Edition { get; init; }
        //public DateOnly? ReleaseDate { get; init; }
        public string Description { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OwnershipLevel OwnershipLevel { get; init; }
        public bool IsInPrint { get; init; }
        public string? ModifierId { get; init; }
        public string? ModifierName { get; init; }
        public DateTime ModifiedDate { get; init; }
        public List<string?> ContributerIds { get; init; }
        public FormatResponse Format { get; init; }
    }
}

