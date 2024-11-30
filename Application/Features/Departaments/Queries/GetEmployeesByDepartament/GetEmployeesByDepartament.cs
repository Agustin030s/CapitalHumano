using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Departaments.Queries.GetEmployeesByDepartament
{
    public class GetEmployeesByDepartament : IRequest<Response<List<EmployeeDto>>>
    {
        public int DepartmentId { get; set; }

        public class GetEmployeesByDepartamentHandler : IRequestHandler<GetEmployeesByDepartament, Response<List<EmployeeDto>>>
        {
            private readonly IRepositoryAsync<Departament> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetEmployeesByDepartamentHandler(IRepositoryAsync<Departament> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<EmployeeDto>>> Handle(GetEmployeesByDepartament request, CancellationToken cancellationToken)
            {
                var spec = new DepartmentWithPositionsAndEmployeesSpec(request.DepartmentId);
                Departament departament = await _repositoryAsync.FirstOrDefaultAsync(spec);

                if(departament == null)
                {
                    return new Response<List<EmployeeDto>>($"El departamento con ID {request.DepartmentId} no existe");
                }

                List<Employee> employees = departament.Positions
                    .SelectMany(p => p.Employees ?? Enumerable.Empty<Employee>())
                    .Distinct()
                    .ToList();
                List<EmployeeDto> dto = _mapper.Map<List<EmployeeDto>>(employees);

                return new Response<List<EmployeeDto>>(dto);
            }

            public class DepartmentWithPositionsAndEmployeesSpec : Specification<Departament>
            {
                public DepartmentWithPositionsAndEmployeesSpec(int departmentId)
                {
                    Query.Where(d => d.Id == departmentId)
                         .Include(d => d.Positions)
                         .ThenInclude(p => p.Employees);
                }
            }
        }
    }
}
