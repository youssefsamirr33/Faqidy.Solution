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
    internal class SocialPostConfigurations : BaseAuditableEntityConfigurations<SocialPost , Guid>
    {
        public override void Configure(EntityTypeBuilder<SocialPost> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Title).HasMaxLength(500);
            builder.Property(s => s.Content).HasMaxLength(30);
            builder.Property(s => s.Location).IsRequired().HasMaxLength(30);
            builder.Property(s => s.PostType).HasConversion(d => d.ToString() , r => (PostType) Enum.Parse(typeof(PostType) , r));

            builder.HasOne(s => s.User)
                .WithMany(s => s.Posts)
                .HasForeignKey(s => s.USerId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(s => s.Child)
                .WithMany(c => c.Posts)
                .HasForeignKey(s => s.ChildId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
