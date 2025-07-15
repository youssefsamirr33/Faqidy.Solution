using Faqidy.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Entities.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Gender gender { get; set; }
        public string? ProfilePicture { get; set; }
        public string? NationalId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; } 
        public bool IsVerified { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
    }
}
