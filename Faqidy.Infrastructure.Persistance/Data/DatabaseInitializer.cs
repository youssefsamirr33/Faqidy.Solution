using Faqidy.Domain.Contract;
using Faqidy.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Infrastructure.Persistance.Data
{
    public class DatabaseInitializer(ApplicationDbContext _context ,UserManager<ApplicationUser> _userMnager) : IDatabaseInitializer
    {
        public async Task Initialize()
        {
            var getpendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (getpendingMigrations.Any())
                await _context.Database.MigrateAsync();
        }

        public async Task Seed()
        {
            if(_userMnager.Users.Count() == 0)
            {
                var user = new ApplicationUser
                {
                    FirstName = "youssef",
                    LastName = "samir",
                    Email = "youssef.samir@gmail.com",
                    UserName = "youssef.samir"
                };

                await _userMnager.CreateAsync(user, "Admin@123");
            }
        }
    }
}
