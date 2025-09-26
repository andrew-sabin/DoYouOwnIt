using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.ProductCategory
{
    public class ProductCategoryRequest
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}
