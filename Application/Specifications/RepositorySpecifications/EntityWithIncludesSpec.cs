using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.RepositorySpecifications
{
    public class EntityWithIncludesSpec<TEntity, TProperty> : Specification<TEntity> where TEntity : class
    {
        public EntityWithIncludesSpec(Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
        {
            Query.Include(expression);
        }
    }
}
