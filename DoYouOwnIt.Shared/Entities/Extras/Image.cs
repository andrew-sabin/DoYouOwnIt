using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Entities.Extras
{
    public class Image
    {
        public int Id { get; set; }
        //Guid PublicId { get; set; } = Guid.NewGuid();
        public string ImageUrl { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;

        /* Relationships M:1 */
        public int FormatId { get; set; }
        public Format Format { get; set; }
    }
}
