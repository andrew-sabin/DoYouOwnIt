using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Token
{
    public class RefreshTokenRequest
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
