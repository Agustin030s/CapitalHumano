using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.RepositorySpecifications;
using Application.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Employees.Queries.GetEmployeeByIdQuery
{
    public class GetEmployeeByIdQuery : IRequest<Response<EmployeeDto>>
    {
        public int Id { get; set; }

        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDto>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetEmployeeByIdQueryHandler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var includeExpressions = new List<Expression<Func<Employee, IEnumerable<object>>>>
                {
                    p => p.salaries,
                    p => p.employeeSkills,
                    p => p.training,
                    p => p.employeePerformanceEvaluations,
                    p => p.positionHistory
                };

                var spec = new EntitiesByIdWithIncludesSpec<Employee, object>(request.Id, includeExpressions);
                Employee employee = await _repositoryAsync.FirstOrDefaultAsync(spec, cancellationToken);

                if(employee == null)
                {
                    return new Response<EmployeeDto>($"Registro no encontrado con el Id: {request.Id}");
                }
                else
                {
                    EmployeeDto dto = _mapper.Map<EmployeeDto>(employee);

                    return new Response<EmployeeDto>(dto);
                }
            }
        }
    }
}
