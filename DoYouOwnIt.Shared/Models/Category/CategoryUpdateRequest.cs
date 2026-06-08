using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Category
{
    public record struct CategoryUpdateRequest( 
        string Name, 
        string Slug,
        string? Description,
        string? CreatorsTitle,
        string? FormatsTitle,
        string? TypeTitle,
        string? EditionTitle
        );
}
