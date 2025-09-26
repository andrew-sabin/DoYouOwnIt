using Microsoft.AspNetCore.Identity;

namespace DoYouOwnIt.Api.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public string? ProfileImageURL { get; set; }
        public bool IsVerified { get; set; } = false; // Age Verification (For Mature Content) Will Be Purchased Through Stripe or Other Payment Processor
        public bool IsBanned { get; set; } = false;
        public string? BanReason { get; set; }
        public DateTime? BanEndDate { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? WebsiteURL { get; set; }
    }
}
