using Faqidy.Domain.Entities.sotialMediaModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Specification.Posts
{
    public class PostWithUserAndMissingChildAndMissingPhotoSpecifications : BaseSpecification<SocialPost , Guid>
    {
        public PostWithUserAndMissingChildAndMissingPhotoSpecifications(Guid PostId)
            :base(p => p.Id.Equals(PostId))
        {
            AddInclude(p => p.User);
            AddInclude(p => p.Child);
            AddThenInclude("Child.Photos");
        }
    }
}
