using DoYouOwnIt.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DoYouOwnIt.Shared.Entities
{
    public class Availability : BaseEntity
    {
        public int FormatId { get; set; }
        public Format Format { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string URL { get; set; } = string.Empty; // URL to the product in the store, if applicable
        public string CurrencyCode { get; set; } = "USD";
        public string UnitSold { get; set; } = "Per Product"; // e.g., Per Product, Per Episode, Per Season, etc.
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string? LastCheckedBy { get; set; } = string.Empty; // UserID of the last user who checked the price
    }
}
