using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DoYouOwnIt.Shared.Models.User
{
    public record struct UserUpdateRequest(
        [StringLength(50, ErrorMessage ="DisplayName cannot exceed 50 characters.")]
        string? DisplayName,
        DateOnly DateOfBirth,
        string? ProfileImageURL,
        [StringLength(100, ErrorMessage = "Website URL cannot exceed 100 characters.")]
        string? WebsiteURL,
        [StringLength(250, ErrorMessage = "User Bio cannot exceed 250 characters.")]
        string? Bio,
        [EmailAddress]
        string Email
        );
}
