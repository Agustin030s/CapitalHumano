using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IRepositoryAsync<Employee> _employee;
        private readonly IRepositoryAsync<Salary> _salary;
        private readonly IRepositoryAsync<Position> _position;

        public SalaryService(IRepositoryAsync<Employee> employee, IRepositoryAsync<Salary> salary, IRepositoryAsync<Position> position)
        {
            _employee = employee;
            _salary = salary;
            _position = position;
        }

        public async Task ProcessMonthlySalaries()
        {
            List<Employee> employees = await _employee.ListAsync();

            foreach (Employee employee in employees)
            {
                if(employee.PositionId != null)
                {
                    Position position = await _position.GetByIdAsync(employee.PositionId);
                    decimal grossSalary = position.GrossSalary;
                    int discountPercentaje = 10;

                    Salary salary = new Salary
                    {
                        PaymentDate = DateTime.UtcNow,
                        StartDate = DateTime.UtcNow.AddMonths(-1),
                        EndDate = DateTime.UtcNow,
                        DiscountPercentage = discountPercentaje,
                        GrossSalary = grossSalary,
                        EmployeeId = employee.Id,
                    };

                    await _salary.AddAsync(salary);
                }
            }
        }
    }
}
