using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Positions.Commands.CreatePositionCommand
{
    public class CreatePositionCommand : CreatePositionDto, IRequest<Response<int>>
    {
    }

    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Position> _repositoryAsync;
        private readonly IRepositoryAsync<Departament> _departamentAsync;
        private readonly IRepositoryAsync<Skill> _skillAsync;
        private readonly IRepositoryAsync<Domain.Entities.Tasks> _taskRepository;
        private readonly IMapper _mapper;

        public CreatePositionCommandHandler(IRepositoryAsync<Position> repositoryAsync, IMapper mapper, IRepositoryAsync<Departament> departamentAsync, IRepositoryAsync<Skill> skillAsync, IRepositoryAsync<Tasks> taskRepository)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _departamentAsync = departamentAsync;
            _skillAsync = skillAsync;
            _taskRepository = taskRepository;
        }

        public async Task<Response<int>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            Departament departament = await _departamentAsync.GetByIdAsync(request.DepartamentId);
            if(departament == null)
            {
                return new Response<int>("El departamento no existe");
            }

            List<Position> positions = await _repositoryAsync.ListAsync();
            bool positionExists = positions.Any(p => p.DepartamentId == request.DepartamentId 
                && p.Description.Equals(request.Description, StringComparison.OrdinalIgnoreCase) == true);

            if (positionExists)
            {
                return new Response<int>("Ya existe un puesto de trabajo con la misma descripción en este departamento");
            }

            Position newRecord = _mapper.Map<Position>(request);

            newRecord.positionSkills = new List<PositionSkill>();
            if(request.SkillsId != null)
            {
                foreach (var skillId in request.SkillsId)
                {
                    Skill existingSkill = await _skillAsync.GetByIdAsync(skillId);
                    if (existingSkill != null)
                    {
                        newRecord.positionSkills.Add(new PositionSkill { SkillId = skillId, Position = newRecord });
                    }
                    else
                    {
                        return new Response<int>($"El skill con ID {skillId} no existe");
                    }
                }
            }

            newRecord.Tasks = new List<Domain.Entities.Tasks>();
            if(request.TaskDescriptions != null)
            {
                foreach(var taskDescription in request.TaskDescriptions)
                {
                    Domain.Entities.Tasks task = new Domain.Entities.Tasks()
                    {
                        Description = taskDescription
                    };
                    newRecord.Tasks.Add(task);
                }
            }

            Position data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id, "Posición creada con éxito");
        }
    }
}
