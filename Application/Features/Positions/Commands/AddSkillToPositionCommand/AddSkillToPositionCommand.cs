using Application.Interfaces;
using Application.Specifications.IntermediateTables;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Positions.Commands.AddSkillToPositionCommand
{
    public class AddSkillToPositionCommand : IRequest<Response<int>>
    {
        public int PositionId { get; set; }
        public int SkillId { get; set; }
    }
    public class AddSkillToPositionCommandHandler : IRequestHandler<AddSkillToPositionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<PositionSkill> _positionSkillRepository;
        private readonly IRepositoryAsync<Position> _positionRepository;
        private readonly IRepositoryAsync<Skill> _skillRepository;
        private readonly IMapper _mapper;

        public AddSkillToPositionCommandHandler(IRepositoryAsync<PositionSkill> positionSkillRepository, IRepositoryAsync<Position> positionRepository, IRepositoryAsync<Skill> skillRepository, IMapper mapper)
        {
            _positionSkillRepository = positionSkillRepository;
            _positionRepository = positionRepository;
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(AddSkillToPositionCommand request, CancellationToken cancellationToken)
        {
            Position position = await _positionRepository.GetByIdAsync(request.PositionId);

            if(position == null)
            {
                return new Response<int>($"No existe la posición con id: {request.PositionId}");
            }

            Skill skill = await _skillRepository.GetByIdAsync(request.SkillId);

            if (skill == null)
            {
                return new Response<int>($"No existe un skill con id: {request.SkillId}");
            }

            var existingPositionSkill = await _positionSkillRepository.FirstOrDefaultAsync(
                new PositionSkillSpecification(request.PositionId, request.SkillId), cancellationToken);

            if (existingPositionSkill != null)
            {
                return new Response<int>($"El skill {skill.Description} ya esta asignado a {position.Description}");
            }

            var newRecord = _mapper.Map<PositionSkill>(request);

            var data = await _positionSkillRepository.AddAsync(newRecord);

            return new Response<int>(data.Id, $"Skill {skill.Description} asignada con éxito a {position.Description}");
        }
    }
}
