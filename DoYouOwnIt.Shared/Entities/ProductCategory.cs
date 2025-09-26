using DoYouOwnIt.Shared.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoYouOwnIt.Shared.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    [Index(nameof(Category), IsUnique = true)]
    public class ProductCategory : BaseEntity
    {
        public required string Category { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
