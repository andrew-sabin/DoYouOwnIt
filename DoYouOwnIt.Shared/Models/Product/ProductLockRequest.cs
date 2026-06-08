using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Product
{
    public record struct ProductLockRequest(bool IsLocked, string? lockedReason);
}
