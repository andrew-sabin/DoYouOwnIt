using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DoYouOwnIt_Shared.Entities.Extras
{
    public class ContactModel
    {
        [Required, MaxLength(60)]
        public string Name { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, MaxLength(100)]
        public string Subject { get; set; } = "";

        [Required, MinLength(10), MaxLength(1000)]
        public string Message { get; set; } = "";
    }
}
