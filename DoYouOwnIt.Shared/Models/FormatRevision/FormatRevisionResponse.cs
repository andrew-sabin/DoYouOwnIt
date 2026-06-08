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
using DoYouOwnIt.Shared.Models.Format;

namespace DoYouOwnIt.Shared.Models.FormatRevision
{
    public record struct FormatRevisionResponse
    {
        public int Id { get; init; }
        //public string Type { get; init; }
        //public string Edition { get; init; }
        //public DateOnly? ReleaseDate { get; init; }
        public string Description { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OwnershipLevel OwnershipLevel { get; init; }
        public bool IsInPrint { get; init; }
        public bool IsAIAssisted { get; init; }
        public string? AIAssistsWith { get; init; }
        public string? ModifierId { get; init; }
        public string? ModifierName { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime ModifiedDate { get; init; }
        public List<string?> ContributerIds { get; init; }
        public FormatResponse Format { get; init; }
        public int RevisionNumber { get; init; }
        public int? PreviousRevisionId { get; init; }
    }
}

