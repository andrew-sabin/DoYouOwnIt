using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Store
{
    public record struct StoreResponseNoAddress(
        int Id,
        string Name, 
        string? LogoURL, 
        bool Online
        );
}
