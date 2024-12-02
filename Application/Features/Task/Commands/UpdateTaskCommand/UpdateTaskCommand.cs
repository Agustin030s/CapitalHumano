using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Task.Commands.UpdateTaskCommand
{
    public class UpdateTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Tasks> _repositoryAsync;

        public UpdateTaskCommandHandler(IRepositoryAsync<Tasks> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            Tasks task = await _repositoryAsync.GetByIdAsync(request.Id);

            if(task == null)
            {
                return new Response<int>($"Registro no encontrado con el Id: {request.Id}");
            }
            else
            {
                task.Description = request.Description;

                await _repositoryAsync.UpdateAsync(task);

                return new Response<int>(task.Id, "Tarea actualizada con éxito");
            }
        }
    }
}
