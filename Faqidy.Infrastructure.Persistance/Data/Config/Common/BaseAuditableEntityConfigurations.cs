using Faqidy.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faqidy.Infrastructure.Persistance.Data.Config.Common
{
    internal class BaseAuditableEntityConfigurations<TEntity , TKey> : BaseEntityConfigurations<TEntity , TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(b => b.LastModifiedBy).IsRequired().HasMaxLength(100);

            builder.Property(b => b.CreateOn).HasDefaultValue("GETUTCDATE");
            builder.Property(b => b.LastModifiedOn).HasDefaultValue("GETUTCDATE");
        }
    }
}
