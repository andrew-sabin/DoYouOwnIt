using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Store
{
    public record struct StoreCreateRequest(
        string Name,
        string Industry,
        string? LogoURL,
        bool Online,
        string? Street,
        string? City,
        string? State,
        string? PostalCode,
        string? Country,
        string Phone,
        string Email,
        string? WebsiteURL
        );
}
