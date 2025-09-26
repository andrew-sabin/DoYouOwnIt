using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.User
{
    public record struct UserResponse(
        string? DisplayName,
        string UserName,
        string? ProfileImageURL,
        string? WebsiteURL,
        string? Bio,
        string Email,
        bool IsVerified,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool IsBanned,
        DateTime? BanEndDate,
        string? BanReason
        );
}
