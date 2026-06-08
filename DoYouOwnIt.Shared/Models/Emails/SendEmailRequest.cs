using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.Emails
{
    public class SendEmailRequest
    {
        public string ToEmail { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsHtml { get; set; } = false;
    }
}
