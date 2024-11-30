using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Task.Commands.CreateTaskCommand
{
    public class CreateTaskCommand : IRequest<Response<int>>
    {
        public string Description { get; set; }
        public int PositionId { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Response<int>>
    {

        private readonly IRepositoryAsync<Position> _positionAsync;
        private readonly IRepositoryAsync<Tasks> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IRepositoryAsync<Position> positionAsync, IRepositoryAsync<Tasks> repositoryAsync, IMapper mapper)
        {
            _positionAsync = positionAsync;
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            Position position = await _positionAsync.GetByIdAsync(request.PositionId);
            if (position == null)
            {
                return new Response<int>("La posición no existe");
            }
            else
            {
                Tasks newRecord = _mapper.Map<Domain.Entities.Tasks>(request);

                Tasks data = await _repositoryAsync.AddAsync(newRecord);

                return new Response<int>(data.Id, "Tarea creada con éxito");
            }
        }
    }
}
