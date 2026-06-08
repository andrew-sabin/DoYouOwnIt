using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DoYouOwnIt.Api.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50, ErrorMessage ="DisplayName cannot exceed 50 characters.")]
        public string DisplayName { get; set; } = string.Empty;
        public string? ProfileImageURL { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsVerified { get; set; } = false; // Age Verification If Needed (For Mature Content) Will Be Purchased Through Stripe or Other Payment Processor
        public bool IsBanned { get; set; } = false;
        public string? BanReason { get; set; }
        public DateTime? BanEndDate { get; set; }
        [StringLength(250, ErrorMessage = "User Bio cannot exceed 250 characters.")]
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiryTime { get; set; }
        [StringLength(100, ErrorMessage = "Website URL cannot exceed 100 characters.")]
        public string? WebsiteURL { get; set; }
    }
}
