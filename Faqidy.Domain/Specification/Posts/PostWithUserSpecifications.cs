using Faqidy.Domain.Entities.sotialMediaModule;
using Faqidy.Domain.Specification.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Specification.Posts
{
    public class PostWithUserSpecifications : BaseSpecification<SocialPost , Guid>
    {
        public PostWithUserSpecifications(Guid Id)
            :base(p => p.Id.Equals(Id))
        {
            AddInclude(p => p.User);
        }
    }
}
