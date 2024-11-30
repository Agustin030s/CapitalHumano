using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departaments.Commands.UpdateDepartamentCommand
{
    public class UpdateDepartamentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string DepartamentCode { get; set; }
        public string Description { get; set; }
    }

    public class UpdateDepartamentCommandHandler : IRequestHandler<UpdateDepartamentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Departament> _repositoryAsync;

        public UpdateDepartamentCommandHandler(IRepositoryAsync<Departament> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateDepartamentCommand request, CancellationToken cancellationToken)
        {
            Departament departament = await _repositoryAsync.GetByIdAsync(request.Id);
            if (departament == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id: {request.Id}");
            }
            else
            {
                departament.DepartamentCode = request.DepartamentCode;
                departament.Description = request.Description;

                await _repositoryAsync.UpdateAsync(departament);

                return new Response<int>(departament.Id, "Departamento actualizado con éxito");
            }
        }
    }
}
