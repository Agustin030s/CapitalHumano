using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.RepositorySpecifications
{
    public class EntitiesByIdWithIncludesSpec<TEntity, TProperty> : Specification<TEntity> where TEntity : class
    {
        public EntitiesByIdWithIncludesSpec(int Id, IEnumerable<Expression<Func<TEntity, IEnumerable<TProperty>>>> expressions)
        {
            Query.Where(e => EF.Property<int>(e, "Id") == Id);

            foreach(var expression in expressions)
            {
                Query.Include(expression);
            }
        }

        public EntitiesByIdWithIncludesSpec(int id, List<Expression<Func<TEntity, object>>> includeEntities)
        {
            Query.Where(e => EF.Property<int>(e, "Id") == id);

            foreach (var includeExpression in includeEntities)
            {
                Query.Include(includeExpression);
            }
        }
    }
}
