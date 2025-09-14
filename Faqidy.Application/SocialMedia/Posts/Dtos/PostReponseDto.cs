using Faqidy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.Posts.Dtos
{
    public class PostReponseDto
    {
        public required string USerId { get; set; }
        public required string UserFullName { get; set; }
        public required Guid ChildProfileId { get; set; }
        public string Title { get; set; }
        public PostType PostType { get; set; }
        public string Content { get; set; }
        public string Location { get; set; }
    }
}
