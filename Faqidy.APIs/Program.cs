using Faqidy.APIs.Errors;
using Faqidy.APIs.Extentions;
using Faqidy.APIs.Middlewares;
using Faqidy.Application;
using Faqidy.Infrastructure;
using Faqidy.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Faqidy.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services Container
            // Add services to the container.
            builder.Services.AddControllers()
                            .ConfigureApiBehaviorOptions(options =>
                            {
                                options.SuppressModelStateInvalidFilter = false;
                                options.InvalidModelStateResponseFactory = (actioncontext) =>
                                {
                                    var errors = actioncontext.ModelState.Where(p => p.Value!.Errors.Count > 0)
                                    .Select(p => new ValidationError
                                    {
                                        Field = p.Key,
                                        Errors = p.Value!.Errors.Select(e => e.ErrorMessage)
                                    });
                                    return new BadRequestObjectResult(new ApiValidationErrorResponse
                                    {
                                        Errors = errors
                                    });
                                };
                            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configure the persistance services in dependancy injection container
            builder.Services.AddApplicationPersistanceServices(builder.Configuration)
                            .AddApplicationLayerServices()
                            .AddIdentitySystemServices(builder.Configuration)
                            .AddInfrastructureServices(builder.Configuration);
            

            #endregion

            var app = builder.Build();

            #region Database Initializer and Data Seeding 
            await app.Initializers();
            #endregion

            #region Configure HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExeptionHandlerMiddleware();

            app.UseHttpsRedirection();


            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
