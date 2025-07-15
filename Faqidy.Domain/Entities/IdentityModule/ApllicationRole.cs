using Microsoft.AspNetCore.Identity;

namespace Faqidy.Domain.Entities.IdentityModule
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
