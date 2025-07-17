using Faqidy.Domain.Common;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.SotialMediaModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Entities.sotialMediaModule
{
    public class ChildPhoto : BaseEntity<Guid>
    {
        public required string PhotoUrl { get; set; }
        public DateTime UploadDate { get; set; }

        public string ChildId { get; set; }
        public virtual MissingChild Child { get; set; }

        public required string UploadedBy { get; set; }
        public virtual ApplicationUser UploadedByUser { get; set; }

    }
}
