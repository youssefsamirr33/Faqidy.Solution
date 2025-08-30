using Faqidy.Domain.Entities.SotialMediaModule;
using Faqidy.Domain.Specification.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Specification.Child_profile
{
    public class ChildProfileWithPhotosSpecification : BaseSpecification<MissingChild , Guid>
    {
        public ChildProfileWithPhotosSpecification(int pageSize , int pageIndex)
            :base()
        {
            AddInclude(p => p.Photos);
            AddPagination(pageSize, pageIndex);
        }

        
    }
}
