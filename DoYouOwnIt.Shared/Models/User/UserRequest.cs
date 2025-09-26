using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.User
{
    public class UserRequest
    {
        public string? DisplayName { get; set; }
        public string UserName { get; set; } = null!;
        public string? ProfileImageURL { get; set; }
        public string? WebsiteURL { get; set; }
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
