using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Login
{
    public record struct LoginResponse(bool Success, string? Error = null, string? Token = null, string? RefreshToken = null);
}
