using Faqidy.Domain.Entities.IdentityModule;
using Microsoft.EntityFrameworkCore;


namespace Faqidy.Infrastructure.Persistance.Data.Config.Identity
{
    internal class ApplicationRoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(u => u.Description).HasMaxLength(200);
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
