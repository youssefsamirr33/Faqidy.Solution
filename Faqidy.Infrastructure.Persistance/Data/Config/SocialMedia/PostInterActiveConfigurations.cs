using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Enums;
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
    internal class PostInterActiveConfigurations : BaseAuditableEntityConfigurations<PostInterActive , Guid>
    {
        public override void Configure(EntityTypeBuilder<PostInterActive> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.InterActiveType).HasConversion(d => d.ToString() , r => (InterActiveType) Enum.Parse(typeof(InterActiveType), r));

            builder.HasOne(p => p.User)
                .WithMany(u => u.InterActives)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Post)
                .WithMany(p => p.InterAction)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(p => p.PostId);
        }
    }
}
