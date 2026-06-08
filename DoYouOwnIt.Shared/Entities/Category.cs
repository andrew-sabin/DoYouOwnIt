using DoYouOwnIt.Shared.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoYouOwnIt.Shared.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class Category : SoftDeleteableEntity
    {
        [StringLength(50, ErrorMessage = "Category Name cannot exceed 50 characters.")]
        public required string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Slug { get; set; } = string.Empty;
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string? Description { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<FormatType> FormatTypes { get; set; } = new List<FormatType>();
        /* Product Attribute Names */
        [StringLength(50, ErrorMessage = "Creator Title cannot exceed 50 characters.")]
        public string? CreatorsTitle { get; set; } = "Creators"; //Cars, Appliances, Tools = Manufacturer; Games = Developer; Books, Movies, TV Shows = Creator; etc.
        [StringLength(50, ErrorMessage = "Formats Title cannot exceed 50 characters.")]
        public string? FormatsTitle { get; set; } = "Formats"; //Books, Movies, TV Shows = Formats; Games = Platforms; Cars, Appliances, Tools = Brand?; etc.

        /* Format Attribute Names */
        [StringLength(50, ErrorMessage = "Type Title cannot exceed 50 characters.")]
        public string? TypeTitle { get; set; } = "Type"; //Books, Movies, TV Shows = Type; Games = Platform; Appliances = Type/Series?; Cars = Trim?; etc.
        [StringLength(50, ErrorMessage = "Edition Title cannot exceed 50 characters.")]
        public string? EditionTitle { get; set; } = "Edition"; //Cars = Year;
        
    }
}
