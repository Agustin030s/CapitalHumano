using Ardalis.Specification;
using System.Linq.Expressions;

namespace Application.Specifications.RepositorySpecifications
{
    public class EntitiesWithIncludesSpec<TEntity, TProperty> : Specification<TEntity> where TEntity : class
    {
        public EntitiesWithIncludesSpec(IEnumerable<Expression<Func<TEntity, IEnumerable<TProperty>>>> includeExpressions)
        {
            foreach (var includeExpression in includeExpressions)
            {
                Query.Include(includeExpression);
            }
        }
    }
}
