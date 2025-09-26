using DoYouOwnIt.Shared.Models.Format;
using DoYouOwnIt.Shared.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Availability
{
     public record struct AvailabilityCreateRequest(
         int FormatId,
         int StoreId,
         string URL,
         string UnitSold,
         string CurrencyCode,
         decimal Price
         );
}
