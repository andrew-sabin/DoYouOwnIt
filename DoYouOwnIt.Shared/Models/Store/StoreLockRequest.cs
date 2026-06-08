using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Store
{
    public record struct StoreLockRequest(bool IsLocked, string? lockedReason);
}
