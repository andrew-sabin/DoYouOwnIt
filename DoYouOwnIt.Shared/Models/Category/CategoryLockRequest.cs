using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Category
{
    public record struct CategoryLockRequest(bool IsLocked, string? lockedReason);
}
