using Faqidy.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.Posts.Dtos
{
    public class UpdatePostDto
    {
        public string title { get; set; }
        public string content { get; set; }
        public PostType postType { get; set; }
    }
}
