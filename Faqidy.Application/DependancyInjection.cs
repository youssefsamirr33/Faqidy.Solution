using Faqidy.Application.Abstraction.Services;
using Faqidy.Application.Mapping;
using Faqidy.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(configurations =>
            {
                configurations.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddScoped(typeof(IPhotosServices), typeof(PhotosServices));
            return services;
        }
    }
}
