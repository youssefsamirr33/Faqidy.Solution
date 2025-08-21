using Faqidy.Application.Abstraction.DTOs.Auth;
using Faqidy.Domain.Entities.IdentityModule;
using Faqidy.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

namespace Faqidy.APIs.Extentions
{
    public static class IdentityExtention
    {
        public static IServiceCollection AddIdentitySystemServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));

            services.AddIdentity<ApplicationUser , ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;

                options.User.RequireUniqueEmail = true;

                #region register by regular excepresion [password configuration options]
                //options.Password.RequiredLength = 8;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireDigit = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireLowercase = true; 
                #endregion

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(configuerOption =>
            {
                configuerOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidIssuer = configuration["JWT:issuer"],
                    ValidAudience = configuration["JWT:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]!))

                };
            });
            

            return services;
        }
    }
}
