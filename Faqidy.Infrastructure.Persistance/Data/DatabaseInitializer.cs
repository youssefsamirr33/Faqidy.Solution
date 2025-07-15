using Faqidy.Domain.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Infrastructure.Persistance.Data
{
    public class DatabaseInitializer(ApplicationDbContext _context) : IDatabaseInitializer
    {
        public async Task Initialize()
        {
            var getpendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (getpendingMigrations.Any())
                await _context.Database.MigrateAsync();
        }

        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}
