using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.IntermediateTables
{
    public class PositionEmployeeSpecification : Specification<PositionHistory>
    {
        public PositionEmployeeSpecification(int employeeId)
        {
            Query.Where(ph => ph.EmployeeId == employeeId && ph.EndDate == null);
        }
    }
}
