using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Employees.Command.DeleteEmployeeCommand
{
    public class DeleteEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsync;

            public DeleteEmployeeCommandHandler(IRepositoryAsync<Employee> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                Employee employee = await _repositoryAsync.GetByIdAsync(request.Id);

                if (employee == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id: {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(employee);

                    return new Response<int>(employee.Id, "Empleado eliminado con éxito");
                }
            }
        }
    }
}
