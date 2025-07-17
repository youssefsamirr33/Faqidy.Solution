using Faqidy.Domain.Common;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Entities.sotialMediaModule
{
    public class PostInterActive : BaseAuditableEntity<Guid>
    {
        public InterActiveType InterActiveType { get; set; }

        public required Guid PostId { get; set; }
        public virtual SocialPost Post { get; set; }

        public required string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
