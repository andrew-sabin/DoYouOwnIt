using DoYouOwnIt.Shared.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class Store : SoftDeleteableEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string Slug { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string? LogoURL { get; set; } = string.Empty;
        public bool Online { get; set; } = false;
        public string? Street { get; set; } = null;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? WebsiteURL { get; set; } = string.Empty;
        public List<Availability> Availabilities { get; set; } = new List<Availability>();

    }
}
