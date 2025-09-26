using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Entities.Interfaces
{
    public class SoftDeleteableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = null;
    }
}
