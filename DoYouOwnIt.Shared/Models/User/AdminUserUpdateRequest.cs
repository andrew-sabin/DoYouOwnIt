using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.User
{
    public record struct AdminUserUpdateRequest(
        string? DisplayName,
        string? UserName,
        string? ProfileImageURL,
        string? WebsiteURL,
        string? Bio,
        bool IsVerified,
        bool IsBanned,
        DateTime? BanEndDate,
        string? BanReason
        );
}
