using Faqidy.Domain.Common;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Enums;

namespace Faqidy.Domain.Entities.SotialMediaModule
{
    public class MissingChild : BaseAuditableEntity<Guid>
    {
        public string ChildName { get; set; }
        public int AgeAtDisappearance { get; set; }
        public int? CurrentEstimatedAge { get; set; }
        public Gender Gender { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime DisappearanceDate { get; set; }
        public string? DisappearanceLocation { get; set; }
        public string DisappearanceCity { get; set; }
        public string DisappearanceGovernorate { get; set; }
        public string? PhysicalDescription { get; set; }
        public int HeightCM { get; set; }
        public int WeightKM { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string? SkinTone { get; set; }
        public CaseStatus Status { get; set; }
        public bool IsVerified { get; set; }
        public string ContactInfo { get; set; } // json string 

        public string? ReporterId { get; set; } // FK 
        public virtual ApplicationUser? Reporter { get; set; }

        public virtual ICollection<ChildPhoto> Photos { get; set; } = new HashSet<ChildPhoto>();
        public virtual ICollection<SocialPost> Posts { get; set; } = new HashSet<SocialPost>();

    }
}
