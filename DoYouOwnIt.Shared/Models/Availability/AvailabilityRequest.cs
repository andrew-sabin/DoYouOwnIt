using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Availability
{
    public class AvailabilityRequest
    {
        public int Id { get; set; }
        public int FormatId { get; set; }
        public int StoreId {get; set; }
        public string URL { get; set; } = string.Empty; // URL to the product in the store, if applicable
        public string CurrenceyCode { get; set; } = "USD";
        public string UnitSold { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
