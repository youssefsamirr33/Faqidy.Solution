using Faqidy.Domain.Enums;

namespace Faqidy.Application.SocialMedia.Posts.Dtos
{
    public class PostDto
    {
        public required string USerId { get; set; }
        public required Guid ChildProfileId { get; set; }
        public string? Title { get; set; }
        public PostType PostType { get; set; }
        public string? Content { get; set; }
        public string? Location { get; set; }
    }
}
