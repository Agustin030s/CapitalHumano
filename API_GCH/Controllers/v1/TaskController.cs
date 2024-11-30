using Application.Features.Task.Commands.CreateTaskCommand;
using Application.Features.Task.Commands.DeleteTaskCommand;
using Application.Features.Task.Commands.UpdateTaskCommand;
using Application.Features.Task.Queries.GetAllTaskQuery;
using Application.Features.Task.Queries.GetTaskByIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace API_GCH.Controllers.v1
{
    public class TaskController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await Mediator.Send(new GetAllTaskQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await Mediator.Send(new GetTaskByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await Mediator.Send(new DeleteTaskCommand { Id = id });
            return Ok(result);
        }
    }
}
