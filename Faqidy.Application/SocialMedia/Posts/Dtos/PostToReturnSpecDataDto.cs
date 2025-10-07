using Faqidy.Application.SocialMedia.MissingProfile.DTOs;


namespace Faqidy.Application.SocialMedia.Posts.Dtos
{
    public class PostToReturnSpecDataDto
    {
        public Guid PostId { get; set; }
        public string UserFullName { get; set; }
        public string UserPicProfile { get; set; }
        public DateTime PostCreatedOn { get; set; }
        public string Content { get; set; }
        public int CountOfLikes { get; set; }
        public int CountOfComments { get; set; }
        public MissingChildDto ChildProfile { get; set; }

    }
}
