using Faqidy.Domain.Common;
using Faqidy.Domain.Specification.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Contract
{
    public interface IGenaricRepository<TEntity , TKey> where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(bool WithTracking = false);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(IBaseSpecification<TEntity, TKey> spec, bool WithTracking = false);
        Task<TEntity?> GetAsync(TKey Id);
        Task<TEntity?> GetWithSpecAsync(IBaseSpecification<TEntity, TKey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> GetCount();
        Task<int> GetCountForlikesOrComments(Guid PostId);
    }
}
