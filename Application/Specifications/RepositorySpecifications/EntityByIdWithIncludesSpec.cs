using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Specifications.RepositorySpecifications
{
    public class EntityByIdWithIncludesSpec<TEntity, TProperty> : Specification<TEntity> where TEntity : class
    {
        public EntityByIdWithIncludesSpec(int Id, Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
        {
            Query.Where(e => EF.Property<int>(e, "Id") == Id)
                .Include(expression);
        }
    }
}
