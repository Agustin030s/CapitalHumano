using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.RepositorySpecifications;
using Application.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Departaments.Queries.GetDepartamentById
{
    public class GetDepartamentByIdQuery : IRequest<Response<DepartamentDto>>
    {
        public int Id { get; set; }

        public class GetDepartamentByIdQueryHandler : IRequestHandler<GetDepartamentByIdQuery, Response<DepartamentDto>>
        {
            private readonly IRepositoryAsync<Departament> _repositoryAsync;
            private readonly IRepositoryAsync<Position> _positionRepository;
            private readonly IMapper _mapper;

            public GetDepartamentByIdQueryHandler(IRepositoryAsync<Departament> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<DepartamentDto>> Handle(GetDepartamentByIdQuery request, CancellationToken cancellationToken)
            {
                var spec = new EntityByIdWithIncludesSpec<Departament, Position>(request.Id, d => d.Positions);
                Departament departament = await _repositoryAsync.FirstOrDefaultAsync(spec);

                if (departament == null)
                {
                    return new Response<DepartamentDto>($"Registro no encontrado con el Id: {request.Id}");
                }
                else
                {
                    DepartamentDto dto = _mapper.Map<DepartamentDto>(departament);

                    return new Response<DepartamentDto>(dto);
                }
            }
        }
    }
}
