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

            // configure the identity system 
            
            services.AddScoped(typeof(IDatabaseInitializer), typeof(DatabaseInitializer));
            services.AddScoped(typeof(IGenaricRepository<,>), typeof(GenaricRepository<,>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            return services;
        }
    }
}
