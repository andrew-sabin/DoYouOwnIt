using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Category
{
    public record struct CategoryCreateRequest(
        string Name,
        string? Description,
        string? CreatorsTitle,
        string? FormatsTitle,
        string? TypeTitle,
        string? EditionTitle
    );
}
