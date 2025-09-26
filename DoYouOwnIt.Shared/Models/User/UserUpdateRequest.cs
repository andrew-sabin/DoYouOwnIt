using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.User
{
    public record struct UserUpdateRequest(
        string? DisplayName,
        string? ProfileImageURL,
        string? WebsiteURL,
        string? Bio,
        string Email
        );
}
