using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.User
{
    public class UserRoleAddRequest
    {
        public string userName { get; set; } = string.Empty;
        public string addRole { get; set; } = string.Empty;
    }
}
