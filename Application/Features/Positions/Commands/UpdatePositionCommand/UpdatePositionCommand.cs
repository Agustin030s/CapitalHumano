using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Positions.Commands.UpdatePositionCommand
{
    public class UpdatePositionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int GrossSalary { get; set; }
    }

    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Position> _repositoryAsync;

        public UpdatePositionCommandHandler(IRepositoryAsync<Position> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            Position position = await _repositoryAsync.GetByIdAsync(request.Id);

            if (position == null)
            {
                return new Response<int>($"Registro no encontrado con el Id: {request.Id}");
            }
            else
            {
                position.Description = request.Description;
                position.GrossSalary = request.GrossSalary;

                await _repositoryAsync.UpdateAsync(position);

                return new Response<int>(position.Id, "Posición actualizada con éxito");
            }
        }
    }
}
