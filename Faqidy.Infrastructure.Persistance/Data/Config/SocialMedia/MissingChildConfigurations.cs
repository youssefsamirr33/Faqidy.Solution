using Faqidy.Domain.Entities.SotialMediaModule;
using Faqidy.Domain.Enums;
using Faqidy.Infrastructure.Persistance.Data.Config.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faqidy.Infrastructure.Persistance.Data.Config.SocialMedia
{
    internal class MissingChildConfigurations : BaseAuditableEntityConfigurations<MissingChild , Guid>
    {
        public override void Configure(EntityTypeBuilder<MissingChild> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.ChildName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.DisappearanceCity).IsRequired().HasMaxLength(50);
            builder.Property(c => c.DisappearanceGovernorate).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Gender).HasConversion(d => d.ToString(), r => (Gender)Enum.Parse(typeof(Gender), r));
            builder.Property(c => c.Status).HasConversion(d => d.ToString(), r => (CaseStatus)Enum.Parse(typeof(CaseStatus), r));

            builder.HasOne(c => c.Reporter)
                .WithMany(u => u.MissingChilde)
                .HasForeignKey(c => c.ReporterId)
                .OnDelete(DeleteBehavior.SetNull);



        }
    }
}
