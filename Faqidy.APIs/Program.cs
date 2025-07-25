using Faqidy.APIs.Extentions;
using Faqidy.Application;
using Faqidy.Application.Mapping;
using Faqidy.Infrastructure.Persistance;
using Faqidy.Infrastructure.Persistance.Data;
using MediatR;
using System.Reflection;

namespace Faqidy.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services Container
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configure the persistance services in dependancy injection container
            builder.Services.AddApplicationPersistanceServices(builder.Configuration);
            builder.Services.AddApplicationLayerServices();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
