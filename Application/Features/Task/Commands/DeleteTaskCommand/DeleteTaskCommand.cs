using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Task.Commands.DeleteTaskCommand
{
    public class DeleteTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Tasks> _repositoryAsync;

            public DeleteTaskCommandHandler(IRepositoryAsync<Tasks> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await _repositoryAsync.GetByIdAsync(request.Id);

                if (task == null)
                {
                    return new Response<int>($"Registro no encontrado con el Id: {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(task);

                    return new Response<int>(task.Id, "Tarea eliminada con éxito");
                }
            }
        }
    }
}
