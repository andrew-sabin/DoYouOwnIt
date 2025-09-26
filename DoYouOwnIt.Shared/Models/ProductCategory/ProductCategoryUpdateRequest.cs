using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.ProductCategory
{
    public record struct ProductCategoryUpdateRequest( 
        string Category, 
        string Slug,
        string? Description);
}
