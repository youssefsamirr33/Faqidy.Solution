using Faqidy.Domain.Common;
using Faqidy.Domain.Contract;
using Faqidy.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Faqidy.Infrastructure.Persistance.Repositories
{
    internal class GenaricRepository<TEntity, TKey>(ApplicationDbContext _context) : IGenaricRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(bool WithTracking)
            => WithTracking ? await _context.Set<TEntity>().ToListAsync() : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey Id)
            => await _context.Set<TEntity>().FindAsync(Id);

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
    }
}
