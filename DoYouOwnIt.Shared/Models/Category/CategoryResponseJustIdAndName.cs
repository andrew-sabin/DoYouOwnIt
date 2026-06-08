using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt.Shared.Models.Category
{
    public record struct CategoryResponseJustIdAndName
        (int Id, string? Name);
}
