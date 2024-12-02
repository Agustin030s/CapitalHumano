using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.RepositorySpecifications;
using Application.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Positions.Queries.GetAllPositionsQuery
{
    public class GetAllPositionsQuery : IRequest<Response<List<PositionDto>>>
    {
        public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, Response<List<PositionDto>>>
        {
            private readonly IRepositoryAsync<Position> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllPositionsQueryHandler(IRepositoryAsync<Position> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<PositionDto>>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
            {
                var spec = new AllPositionsWithDetailsSpec();

                List<Position> positions = await _repositoryAsync.ListAsync(spec, cancellationToken);
                List<PositionDto> positionDtos = _mapper.Map<List<PositionDto>>(positions);

                return new Response<List<PositionDto>>(positionDtos);
            }

            public class AllPositionsWithDetailsSpec : Specification<Position>
            {
                public AllPositionsWithDetailsSpec()
                {
                    Query.Include(p => p.Tasks)
                         .Include(p => p.Employees)
                         .Include(p => p.positionSkills)
                         .ThenInclude(ps => ps.Skill)
                         .Include(p => p.Departament);
                }
            }
        }
    }
}
