using Application.Features.Departaments.Commands.CreateDepartamentCommand;
using Application.Features.Departaments.Commands.DeleteDepartamentCommand;
using Application.Features.Departaments.Commands.UpdateDepartamentCommand;
using Application.Features.Departaments.Queries.GetAllDepartaments;
using Application.Features.Departaments.Queries.GetDepartamentById;
using Application.Features.Departaments.Queries.GetEmployeesByDepartament;
using Microsoft.AspNetCore.Mvc;

namespace API_GCH.Controllers.v1
{
    public class DepartamentController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDepartaments()
        {
            var result = await Mediator.Send(new GetAllDepartamentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartamentById(int id)
        {
            var result = await Mediator.Send(new GetDepartamentByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpGet("filter/{id}")]
        public async Task<IActionResult> FilterEmployeesByDepartament(int id)
        {
            var result = await Mediator.Send(new GetEmployeesByDepartament { DepartmentId = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartament(CreateDepartamentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartament(int id, UpdateDepartamentCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartament(int id)
        {
            var result = await Mediator.Send(new DeleteDepartamentCommand { Id = id });
            return Ok(result);
        }
    }
}
