using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Positions.Commands.DeletePositionCommand
{
    public class DeletePositionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Position> _repositoryAsync;

            public DeletePositionCommandHandler(IRepositoryAsync<Position> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
            {
                Position position = await _repositoryAsync.GetByIdAsync(request.Id);

                if(position == null)
                {
                    return new Response<int>($"Registro no encontrado con el Id: {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(position);

                    return new Response<int>(position.Id, "Posición eliminada con éxito");
                }
            }
        }
    }
}
