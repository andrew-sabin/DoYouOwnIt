using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.ProductCategory
{
    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
    }
}
