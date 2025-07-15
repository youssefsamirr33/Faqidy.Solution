using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Common
{
    public class BaseAuditableEntity<TKey> : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual string CreatedBy { get; set; } = null!;
        public DateTime CreateOn { get; set; }
        public virtual string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
