using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.RepositorySpecifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departaments.Queries.GetAllDepartaments
{
    public class GetAllDepartamentsQuery : IRequest<Response<List<DepartamentDto>>>
    {
        public class GetAllDepartamentsQueryHandler : IRequestHandler<GetAllDepartamentsQuery, Response<List<DepartamentDto>>>
        {
            private readonly IRepositoryAsync<Departament> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetAllDepartamentsQueryHandler(IRepositoryAsync<Departament> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<DepartamentDto>>> Handle(GetAllDepartamentsQuery request, CancellationToken cancellationToken)
            {
                EntityWithIncludesSpec<Departament, Position> spec = new EntityWithIncludesSpec<Departament, Position>(d => d.Positions);
                List<Departament> departamentsList = await _repositoryAsync.ListAsync(spec, cancellationToken);

                List<DepartamentDto> departamentDto = _mapper.Map<List<DepartamentDto>>(departamentsList);

                return new Response<List<DepartamentDto>>(departamentDto);
            }
        }
    }
}
