using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Entities.SotialMediaModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Faqidy.Infrastructure.Persistance.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser , ApplicationRole , string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<ChildPhoto> ChildPhotos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MissingChild> MissingChildrens { get; set; }
        public DbSet<PostInterActive> PostInterActives { get; set; }
        public DbSet<SocialPost> SocialPosts { get; set; }

    }
}
