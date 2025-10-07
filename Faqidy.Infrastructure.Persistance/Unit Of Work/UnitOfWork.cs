using Faqidy.Domain.Common;
using Faqidy.Domain.Contract;
using Faqidy.Infrastructure.Persistance.Data;
using Faqidy.Infrastructure.Persistance.Repositories;

namespace Faqidy.Infrastructure.Persistance.Unit_Of_Work
{
    public class UnitOfWork(ApplicationDbContext _context) : IUnitOfWork

    {
        private readonly Dictionary<string , object> _repository = new Dictionary<string , object>();


        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
           where TEntity : BaseAuditableEntity<TKey>
           where TKey : IEquatable<TKey>
        {
            var KeyName = typeof(TEntity).Name;
            if(_repository.ContainsKey(KeyName))
                return (IGenaricRepository<TEntity ,TKey>) _repository[KeyName];
            var repo = new GenaricRepository<TEntity, TKey>(_context);
            _repository.Add(KeyName,repo);
            return repo;    
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

    }
}
