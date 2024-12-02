using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departaments.Commands.DeleteDepartamentCommand
{
    public class DeleteDepartamentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class DeleteDepartamentCommandHandler : IRequestHandler<DeleteDepartamentCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Departament> _repositoryAsync;

            public DeleteDepartamentCommandHandler(IRepositoryAsync<Departament> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteDepartamentCommand request, CancellationToken cancellationToken)
            {
                Departament departament = await _repositoryAsync.GetByIdAsync(request.Id);

                if(departament == null)
                {
                    return new Response<int>($"Registro no encontrado con el Id: {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(departament);

                    return new Response<int>(departament.Id, "Departamento eliminado con éxito");
                }
            }
        }
    }
}
