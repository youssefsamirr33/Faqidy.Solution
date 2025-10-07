using Faqidy.Domain.Common;
using Faqidy.Domain.Specification.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Domain.Specification
{
    public class BaseSpecification<TEntity, Tkey> : IBaseSpecification<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<TEntity, object>>> include { get; set; } = new ();
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set ; }
        public Expression<Func<TEntity, TEntity>> Select { get; set; } = null!;
        public bool IsProjection { get; set ; }
        public List<string> thenInclude { get; set; } = new();

        protected BaseSpecification()
        {
            
        }

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria; 
        }

        private protected  void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            include.Add(expression);
        }

        private protected void AddThenInclude(string expression)
        {
            thenInclude.Add(expression);
        }

        private protected void AddPagination(int pageSize , int pageIndex)
        {
            IsPagination = true;
            Take = pageSize;
            Skip = pageSize * (pageIndex - 1);
        }

        private protected void AddProjection(Expression<Func<TEntity, TEntity>> expression)
        {
            IsProjection = true;
            Select = expression;    
        }
        
    }
}
