using DoYouOwnIt.Shared.Models.Format;
using DoYouOwnIt.Shared.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Availability
{
     public record struct AvailabilityResponse(
         int Id,
         string URL,
         string CurrencyCode,
         decimal Price,
         string UnitSold,
         string? LastCheckedBy,
         FormatResponseNoProduct Format,
         StoreResponse Store
         );
}
