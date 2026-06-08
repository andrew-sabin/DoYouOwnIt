using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DoYouOwnIt_Shared.Models.Emails
{
    public class ResetPasswordRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password", ErrorMessage = "Passwords Do Not Match")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
