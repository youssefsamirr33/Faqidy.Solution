using Faqidy.Domain.Common;
using Faqidy.Domain.Specification.contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Infrastructure.Persistance.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity , Tkey>(IQueryable<TEntity> inputQuery , IBaseSpecification<TEntity ,Tkey> spec)
            where TEntity : BaseEntity<Tkey>
            where Tkey : IEquatable<Tkey>
        {
            var query = inputQuery;  // _dbcotnext.set<TEntity>()

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);  // _dbcotnext.set<TEntity>().Where(p => p.id == id)

           

            query = spec.include.Aggregate(query, (crrunetQuery, includeExpression) => crrunetQuery.Include(includeExpression));

            query = spec.thenInclude.Aggregate(query, (crrunetQuery, ThenIncludeExpression) => crrunetQuery.Include(ThenIncludeExpression));

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            if (spec.IsProjection)
            {
                query = query.Select(spec.Select);
            }

            return query;

        }
    }
}
