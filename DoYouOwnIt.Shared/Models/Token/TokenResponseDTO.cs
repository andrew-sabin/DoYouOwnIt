using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Token
{
    public class TokenResponseDTO
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
