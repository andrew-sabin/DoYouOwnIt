using System;
using System.Collections.Generic;
using System.Text;

namespace DoYouOwnIt_Shared.Models.Emails
{
    public record struct ResetPasswordResponse(bool Success, IEnumerable<string>? Errors = null);
}
