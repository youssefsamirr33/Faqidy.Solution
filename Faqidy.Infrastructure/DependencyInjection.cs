using Faqidy.Domain.Contract.Redis_Repo;
using Faqidy.Infrastructure.Redis_Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped(typeof(IConnectionMultiplexer), (factory) =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                var connectionMultiPlexerObj = ConnectionMultiplexer.ConnectAsync(connectionString!);
                return connectionMultiPlexerObj;
            });

            services.AddScoped(typeof(IRedisRepository), typeof(RedisRepository));

            return services;
        }
    }
}
