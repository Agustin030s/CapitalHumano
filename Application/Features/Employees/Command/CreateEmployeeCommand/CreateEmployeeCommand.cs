using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Employees.Command.CreateEmployeeCommand
{
    public class CreateEmployeeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PositionId { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;
        private readonly IRepositoryAsync<Position> _positionRepositoryAsync;
        private readonly IRepositoryAsync<PositionHistory> _positionHistoryRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper, 
            IRepositoryAsync<Position> positionRepositoryAsync, IRepositoryAsync<PositionHistory> positionHistoryRepositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _positionRepositoryAsync = positionRepositoryAsync;
            _positionHistoryRepositoryAsync = positionHistoryRepositoryAsync;
        }

        public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Position position = await _positionRepositoryAsync.GetByIdAsync(request.PositionId);
            if(position == null)
            {
                return new Response<int>("La posición de trabajo no existe");
            }
            else
            {
                Employee newRecord = _mapper.Map<Employee>(request);

                Employee data = await _repositoryAsync.AddAsync(newRecord);

                PositionHistory positionHistory = new PositionHistory
                {
                    StartDate = DateTime.UtcNow,
                    EmployeeId = data.Id,
                    PositionId = data.PositionId
                };

                await _positionHistoryRepositoryAsync.AddAsync(positionHistory);

                return new Response<int>(data.Id, "Empleado creado con éxito");
            }
        }
    }
}
