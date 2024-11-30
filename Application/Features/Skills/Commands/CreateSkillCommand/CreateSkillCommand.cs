using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Skills.Commands.CreateSkillCommand
{
    public class CreateSkillCommand : IRequest<Response<int>>
    {
        public string Description { get; set; }
    }

    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Skill> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateSkillCommandHandler(IRepositoryAsync<Skill> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            Skill newRecord = _mapper.Map<Skill>(request);

            Skill data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id, "Skill creada con éxito");
        }
    }
}
