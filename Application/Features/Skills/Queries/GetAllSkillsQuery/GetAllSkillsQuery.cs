using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Skills.Queries.GetAllSkillsQuery
{
    public class GetAllSkillsQuery : IRequest<Response<List<SkillDto>>>
    {
        public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, Response<List<SkillDto>>>
        {
            private readonly IRepositoryAsync<Skill> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllSkillsQueryHandler(IRepositoryAsync<Skill> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<SkillDto>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
            {
                List<Skill> skills = await _repositoryAsync.ListAsync();

                List<SkillDto> dto = _mapper.Map<List<SkillDto>>(skills);

                return new Response<List<SkillDto>>(dto);
            }
        }
    }
}
