using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faqidy.Infrastructure.Persistance.Data.Config.Identity
{
    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Address).HasMaxLength(100);
            builder.Property(u => u.City).HasMaxLength(100);

            builder.Property(u => u.gender).HasConversion(v => v.ToString() , v => (Gender)Enum.Parse(typeof(Gender), v));

            builder.Property(u => u.NationalId).HasMaxLength(100);
            builder.Property(u => u.Country).HasMaxLength(100).HasDefaultValue("Egypt");
            builder.Property(u => u.CreatedAt).HasDefaultValue("GETUTCDATE");
            builder.Property(u => u.UpdatedAt).HasDefaultValue("GETUTCDATE");
            builder.Property(u => u.IsVerified).HasDefaultValue<bool>(false);
        }
    }
}
