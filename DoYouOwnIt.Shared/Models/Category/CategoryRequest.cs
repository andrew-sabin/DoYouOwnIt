using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Category
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsLocked { get; set; }
        public string? lockedReason { get; set; }
        public string? CreatorsTitle { get; set; }
        public string? FormatsTitle { get; set; }
        public string? TypeTitle { get; set; }
        public string? EditionTitle { get; set; }
    }
}
