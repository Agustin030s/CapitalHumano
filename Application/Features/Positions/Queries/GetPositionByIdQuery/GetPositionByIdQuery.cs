﻿using Application.DTOs;
using Application.Interfaces;
using Application.Specifications.RepositorySpecifications;
using Application.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.Positions.Queries.GetPositionByIdQuery
{
    public class GetPositionByIdQuery : IRequest<Response<PositionDto>>
    {
        public int Id { get; set; }
        public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Response<PositionDto>>
        {
            private readonly IRepositoryAsync<Position> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetPositionByIdQueryHandler(IRepositoryAsync<Position> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<PositionDto>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
            {
                var includeExpressions = new List<Expression<Func<Position, IEnumerable<object>>>>
                {
                    p => p.Tasks,
                    p => p.Employees,
                    p => p.positionSkills,
                };

                var includeEntities = new List<Expression<Func<Position, object>>>
                {
                    p => p.Departament,
                };

                var spec = new EntitiesByIdWithIncludesSpec<Position, object>(request.Id, includeExpressions);
                foreach(var include in includeEntities)
                {
                    spec.Query.Include(include);
                }

                Position position = await _repositoryAsync.FirstOrDefaultAsync(spec, cancellationToken);

                if(position == null)
                {
                    return new Response<PositionDto>($"Registro no encontrado con el Id: {request.Id}");
                }
                else
                {
                    PositionDto dto = _mapper.Map<PositionDto>(position);

                    return new Response<PositionDto>(dto);
                }
            }
        }
    }
}
