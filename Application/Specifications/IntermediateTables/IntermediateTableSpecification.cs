using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Specifications.IntermediateTables
{
    public class IntermediateTableSpecification<T> : Specification<T> where T : class
    {
        public IntermediateTableSpecification(int firstId, int secondId, 
            Expression<Func<T, int>> firstIdProperty, Expression<Func<T, int>> secondIdProperty)
        {
            Query.Where(e => EF.Property<int>(e, firstIdProperty.Body.ToString()) == firstId &&
                EF.Property<int>(e, secondIdProperty.Body.ToString()) == secondId);
        }
    }
}
