using Faqidy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Specification.contract
{
    public interface IBaseSpecification<TEntity , Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        public Expression<Func<TEntity , bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity , object>>> include { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set; } 
    }
}
