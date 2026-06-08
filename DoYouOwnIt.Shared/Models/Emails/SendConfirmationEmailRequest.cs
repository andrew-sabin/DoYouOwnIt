using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.Emails
{
    public class SendConfirmationEmailRequest
    {
        public string userName {  get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
        public string ToEmail { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsHtml { get; set; } = false;
    }
}
