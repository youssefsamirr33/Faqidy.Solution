using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.SocialMedia.MissingProfile.DTOs
{
    public class ChildPhotoDto
    {
        public Guid Id { get; set; }
        public required string PhotoUrl { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
