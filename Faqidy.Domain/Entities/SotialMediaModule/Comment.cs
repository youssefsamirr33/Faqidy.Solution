using Faqidy.Domain.Common;
using Faqidy.Domain.Entities.IdentityModule;

namespace Faqidy.Domain.Entities.sotialMediaModule
{
    public class Comment : BaseAuditableEntity<Guid>
    {
        public string? Content { get; set; }
        public bool IsHelpful { get; set; } 
        public bool IsVerifiedInfo { get; set; } 
        public int LikkeCounts { get; set; }

        public required Guid PostId { get; set; }
        public virtual SocialPost Post { get; set; }
        public required string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Guid ParantCommentId { get; set; }
        public virtual Comment parantComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new HashSet<Comment>();
    }
}
