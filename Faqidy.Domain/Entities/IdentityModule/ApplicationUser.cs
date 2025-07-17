using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Entities.SotialMediaModule;
using Faqidy.Domain.Enums;
using Microsoft.AspNetCore.Identity;
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

        public virtual ICollection<MissingChild> MissingChilde { get; set; } = new HashSet<MissingChild>();
        public virtual ICollection<SocialPost> Posts { get; set; } = new HashSet<SocialPost>();
        public virtual ICollection<PostInterActive> InterActives { get; set; } = new HashSet<PostInterActive>();
        public virtual ICollection<Comment>? Comments { get; set; } = new HashSet<Comment>();
        

    }
}
