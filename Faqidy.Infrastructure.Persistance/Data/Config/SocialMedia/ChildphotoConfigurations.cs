using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Infrastructure.Persistance.Data.Config.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faqidy.Infrastructure.Persistance.Data.Config.SocialMedia
{
    internal class ChildphotoConfigurations : BaseEntityConfigurations<ChildPhoto , Guid>
    {
        public override void Configure(EntityTypeBuilder<ChildPhoto> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.PhotoUrl).IsRequired();
            builder.Property(p => p.UploadDate).HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(p => p.Child)
                .WithMany(c => c.Photos)
                .HasForeignKey(p => p.ChildId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.UploadedByUser)
                .WithMany()
                .HasForeignKey(p => p.UploadedBy)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
