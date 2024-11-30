using Application.Interfaces;
using Application.Specifications.IntermediateTables;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Employees.Command.UpdateEmployeePositionCommand
{
    public class UpdateEmployeePositionCommand : IRequest<Response<int>>
    {
        public int EmployeeId { get; set; }
        public int? PositionId { get; set; }
        public EndReason? EndReason { get; set; }
        public string? Observations { get; set; }
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
    }

    public class UpdateEmployeePositionCommandHandler : IRequestHandler<UpdateEmployeePositionCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Employee> _employeeRepository;
        private readonly IRepositoryAsync<PositionHistory> _history;
        private readonly IRepositoryAsync<Position> _positionRepository;

        public UpdateEmployeePositionCommandHandler(IRepositoryAsync<Employee> employeeRepository, 
            IRepositoryAsync<PositionHistory> history, IRepositoryAsync<Position> positionRepository)
        {
            _employeeRepository = employeeRepository;
            _history = history;
            _positionRepository = positionRepository;
        }

        public async Task<Response<int>> Handle(UpdateEmployeePositionCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (employee == null)
            {
                return new Response<int>("Empleado no encontrado");
            }

            PositionHistory currentPosition = await _history.FirstOrDefaultAsync(new PositionEmployeeSpecification(employee.Id));

            if(currentPosition != null)
            {
                currentPosition.EndDate = request.EndDate;
                currentPosition.endReason = request.EndReason;
                currentPosition.Observations = request.Observations;
                await _history.UpdateAsync(currentPosition);
            }

            if(request.EndReason == EndReason.Resignation || request.EndReason == EndReason.Termination)
            {
                employee.PositionId = null;
            }
            else if(request.EndReason == EndReason.Transfer)
            {
                Position newPosition = await _positionRepository.GetByIdAsync(request.PositionId);
                if(newPosition == null)
                {
                    return new Response<int>("Posición de trabajo no encontrada");
                }

                employee.PositionId = newPosition.Id;

                PositionHistory newPositionHistory = new PositionHistory
                {
                    StartDate = DateTime.UtcNow,
                    PositionId = newPosition.Id,
                    EmployeeId = employee.Id,
                };

                await _history.AddAsync(newPositionHistory);
            }

            await _employeeRepository.UpdateAsync(employee);

            return new Response<int>(employee.Id, "Posicion de trabajo del empleado actualizada con éxito");
        }
    }
}
