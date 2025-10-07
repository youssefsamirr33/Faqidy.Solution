using Faqidy.Domain.Common;

namespace Faqidy.Domain.Contract
{
    public interface IUnitOfWork : IAsyncDisposable
        
    {
        IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>;

        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
