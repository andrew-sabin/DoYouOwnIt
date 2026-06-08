using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.User
{
    public class UserRequest
    {
        [StringLength(50, ErrorMessage = "Display Name cannot exceed 50 characters.")]
        public string? DisplayName { get; set; }
        public string UserName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string? ProfileImageURL { get; set; }
        [StringLength(100, ErrorMessage = "Website URL cannot exceed 100 characters.")]
        public string? WebsiteURL { get; set; }
        [StringLength(250, ErrorMessage = "User Bio cannot exceed 250 characters.")]
        public string? Bio { get; set; }
        public string Email { get; set; } = null!;
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? BanEndDate { get; set; }
        public string? BanReason { get; set; }
    }
}
