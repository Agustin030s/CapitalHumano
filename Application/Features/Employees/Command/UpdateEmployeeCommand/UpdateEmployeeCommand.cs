using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Employees.Command.UpdateEmployeeCommand
{
    public class UpdateEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Genero Sexo { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;
        private readonly IRepositoryAsync<Position> _positionRepositoryAsync;

        public UpdateEmployeeCommandHandler(IRepositoryAsync<Employee> repositoryAsync, IRepositoryAsync<Position> positionRepositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
            _positionRepositoryAsync = positionRepositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _repositoryAsync.GetByIdAsync(request.Id);
            if(employee == null)
            {
                return new Response<int>($"Registro no encontrado con el Id: {request.Id}");
            }
            else
            {
                employee.Name = request.Name;
                employee.LastName = request.LastName;
                employee.Phone = request.Phone;
                employee.Email = request.Email;
                employee.Address = request.Address;

                await _repositoryAsync.UpdateAsync(employee);

                return new Response<int>(employee.Id, "Empleado actualizado con éxito");
            }
        }
    }
}
