using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsLocked { get; init; }
        public string? CreatorsTitle { get; init; }
        public string? FormatsTitle { get; init; }
        public string? TypeTitle { get; init; }
        public string? EditionTitle { get; init; }
        public string? lockedReason { get; init; }
        public string? LockedByUser { get; init; }
        public DateTime? LockedDate { get; init; }
    }
}
