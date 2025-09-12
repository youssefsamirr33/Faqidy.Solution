using Faqidy.Domain.Entities.sotialMediaModule;
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


        public ChildProfileWithPhotosSpecification(Guid Id)
            :base(p => p.Id.Equals(Id)) 
        {
            AddProjection(m => new MissingChild
            {
                Id = m.Id,
                ChildName = m.ChildName,
                Status = m.Status,
                AgeAtDisappearance = m.AgeAtDisappearance,
                BirthDate = m.BirthDate,
                Gender = m.Gender,
                DisappearanceLocation = m.DisappearanceLocation,
                Photos = m.Photos.Select(p => new ChildPhoto
                {
                    Id = p.Id,
                    PhotoUrl = p.PhotoUrl,
                }).ToList()
            });
        }


    }
}
