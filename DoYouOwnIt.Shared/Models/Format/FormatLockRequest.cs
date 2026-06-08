using DoYouOwnIt.Shared.Enums;
using DoYouOwnIt.Shared.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Format
{
    public record struct FormatLockRequest(bool IsLocked, string? lockedReason);
}
