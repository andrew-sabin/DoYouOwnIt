using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Token
{
    public record struct TokenRequestDTO(string Token, string RefreshToken);
}
