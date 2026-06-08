using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.Emails
{
    public class EmailResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
