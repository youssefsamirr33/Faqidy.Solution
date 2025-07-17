using Faqidy.Domain.Common;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.SotialMediaModule;
using Faqidy.Domain.Enums;

namespace Faqidy.Domain.Entities.sotialMediaModule
{
    public class SocialPost : BaseAuditableEntity<Guid>
    {
        public string? Title { get; set; }
        public PostType PostType { get; set; }
        public string? Content { get; set; }
        public string Location { get; set; }
        public int LikeCounts { get; set; }
        public int CommentsCounts { get; set; }
        public int ShareCounts { get; set; }
        public int ViewCounts { get; set; }

        public string? USerId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public Guid? ChildId { get; set; }
        public virtual MissingChild? Child { get; set; }
        public virtual ICollection<PostInterActive> InterAction { get; set; } = new HashSet<PostInterActive>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();


    }
}
