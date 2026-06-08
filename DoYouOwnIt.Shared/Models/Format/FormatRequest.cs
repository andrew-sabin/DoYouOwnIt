using DoYouOwnIt.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Format
{
    public class FormatRequest
    {
        public string Type { get; set; } = string.Empty;
        public int FormatTypeId { get; set; }
        public string Edition { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public bool IsLocked { get; set; }
        public string? lockedReason { get; set; }
        public string? LockedByUser { get; set; } = string.Empty;
        public DateTime? LockedDate { get; set; } = null;
        public string? CoverImageUrl { get; set; } = string.Empty;
        public DateOnly? ReleaseDate { get; set; } = null;
        //public string Description { get; set; } = string.Empty;
        //public required OwnershipLevel OwnershipLevel { get; set; }
        //public bool IsInPrint { get; set; }
        //public bool IsAIAssisted { get; set; }
        //public string? AIAssistsWith { get; set; }
        public string? CreatorId { get; set; }
        //public string? ModifierId { get; set; }
        //public string? ModifierName { get; set; }
        //public DateTime ModifiedDate { get; set; }
        public int ProductId { get; set; }
        public int formatrevisionid { get; set; }
    }
}
