using Ardalis.Specification;
using System.Linq.Expressions;

namespace Application.Specifications.RepositorySpecifications
{
    public class ProjectionSpec<T, TResult> : Specification<T, TResult> where T : class
    {
        public ProjectionSpec(
            Expression<Func<T, TResult>> selector,
            Action<ISpecificationBuilder<T>> includeConfig = null)
        {
            includeConfig?.Invoke(Query);

            Query.Select(selector);
        }
    }
}
