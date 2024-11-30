using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Employees.Queries.GetAllEmployeesQuery
{
    public class GetAllEmployeesQuery : IRequest<Response<List<EmployeeDto>>>
    {
        public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Response<List<EmployeeDto>>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllEmployeesQueryHandler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
            {
                var spec = new AllEmployiesWithDetailsSpec();
                List<Employee> employees = await _repositoryAsync.ListAsync(spec, cancellationToken);

                List<EmployeeDto> dto = _mapper.Map<List<EmployeeDto>>(employees);

                return new Response<List<EmployeeDto>>(dto);
            }

            public class AllEmployiesWithDetailsSpec : Specification<Employee>
            {
                public AllEmployiesWithDetailsSpec()
                {
                    Query.Include(p => p.salaries)
                        .Include(p => p.training)
                        .Include(p => p.employeeSkills)
                        .Include(p => p.employeePerformanceEvaluations)
                        .Include(p => p.positionHistory);
                }
            }
        }
    }
}
