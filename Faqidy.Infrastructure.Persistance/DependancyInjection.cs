using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Infrastructure.Persistance.Data;
using Faqidy.Infrastructure.Persistance.Repositories;
using Faqidy.Infrastructure.Persistance.Unit_Of_Work;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Faqidy.Infrastructure.Persistance
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationPersistanceServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.SignIn.RequireConfirmedAccount = true;

                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IDatabaseInitializer), typeof(DatabaseInitializer));
            services.AddScoped(typeof(IGenaricRepository<,>), typeof(GenaricRepository<,>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            return services;
        }
    }
}
