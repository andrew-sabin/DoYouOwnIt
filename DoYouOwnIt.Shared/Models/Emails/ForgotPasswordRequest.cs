using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.Emails
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
    
