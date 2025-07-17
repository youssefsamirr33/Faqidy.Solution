using Faqidy.Domain.Common;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Infrastructure.Persistance.Data.Config.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Infrastructure.Persistance.Data.Config.SocialMedia
{
    internal class CommentsConfigurations : BaseAuditableEntityConfigurations<Comment , Guid>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Content).HasMaxLength(500);
            builder.Property(c => c.IsVerifiedInfo).HasDefaultValue(false);
            builder.Property(c => c.IsHelpful).HasDefaultValue(false);

            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
