using Application.Features.Employees.Command.CreateEmployeeCommand;
using Application.Features.Employees.Command.DeleteEmployeeCommand;
using Application.Features.Employees.Command.UpdateEmployeeCommand;
using Application.Features.Employees.Command.UpdateEmployeePositionCommand;
using Application.Features.Employees.Queries.GetAllEmployeesQuery;
using Application.Features.Employees.Queries.GetEmployeeByIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace API_GCH.Controllers.v1
{
    public class EmployeeController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await Mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await Mediator.Send(new GetEmployeeByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update-position/{employeeId}")]
        public async Task<IActionResult> UpdateEmployeePosition(int employeeId, UpdateEmployeePositionCommand command)
        {
            if (employeeId != command.EmployeeId)
                return BadRequest();

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            return Ok(result);
        }
    }
}
