using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Skills.Queries.GetSkillByIdQuery
{
    public class GetSkillByIdQuery : IRequest<Response<SkillDto>>
    {
        public int Id { get; set; }
        public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, Response<SkillDto>>
        {
            private readonly IRepositoryAsync<Skill> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetSkillByIdQueryHandler(IRepositoryAsync<Skill> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<SkillDto>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
            {
                Skill skill = await _repositoryAsync.GetByIdAsync(request.Id);

                if (skill == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id: {request.Id}");
                }
                else
                {
                    SkillDto dto = _mapper.Map<SkillDto>(skill);

                    return new Response<SkillDto>(dto);
                }
            }
        }
    }
}
