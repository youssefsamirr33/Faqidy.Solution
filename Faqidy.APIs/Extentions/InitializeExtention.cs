using Faqidy.Domain.Contract;
using Faqidy.Infrastructure.Persistance.Data;

namespace Faqidy.APIs.Extentions
{
    public static class InitializeExtention
    {
        public static async Task<WebApplication> Initializers(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _dbInitializer = service.GetRequiredService<IDatabaseInitializer>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbInitializer.Initialize();
                await _dbInitializer.Seed();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "That Error When occured apply the migrations [ update databas or seeding data]");
            }

            return app;
        }
    }
}
