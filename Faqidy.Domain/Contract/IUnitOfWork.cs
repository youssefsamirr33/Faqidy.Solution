using Faqidy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Contract
{
    public interface IUnitOfWork : IAsyncDisposable
        
    {
        IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>;

        Task<int> CompleteAsync();
    }
}
