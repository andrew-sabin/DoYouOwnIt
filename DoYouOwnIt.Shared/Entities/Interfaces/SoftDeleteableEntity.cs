using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Entities.Interfaces
{
    public class SoftDeleteableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = null;
        public bool IsLocked { get; set; } = false; // Can Only be edited by Admins and Moderators
        [MaxLength(150)]
        public string? lockedReason { get; set; } = string.Empty;
        public string? LockedByUser { get; set; } = string.Empty; // UserId for locking the Format
        public DateTime? LockedDate { get; set; } = null;
        public bool HasIssue { get; set; }
        [MaxLength(150)]
        public string? Issue { get; set; }
        [MaxLength(100)]
        public string? IssueURL { get; set; }

    }
}
