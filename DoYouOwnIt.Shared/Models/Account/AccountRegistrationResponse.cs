using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Account
{
    public record struct AccountRegistrationResponse(bool Success, IEnumerable<string>? Errors = null);
}
