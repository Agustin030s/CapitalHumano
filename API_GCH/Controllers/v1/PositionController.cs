using Application.Features.Positions.Commands.AddSkillToPositionCommand;
using Application.Features.Positions.Commands.CreatePositionCommand;
using Application.Features.Positions.Commands.DeletePositionCommand;
using Application.Features.Positions.Commands.UpdatePositionCommand;
using Application.Features.Positions.Queries.GetAllPositionsQuery;
using Application.Features.Positions.Queries.GetPositionByIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace API_GCH.Controllers.v1
{
    public class PositionController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPositions()
        {
            var result = await Mediator.Send(new GetAllPositionsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionById(int id)
        {
            var result = await Mediator.Send(new GetPositionByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePosition(CreatePositionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{positionId}/skill/{skillId}")]
        public async Task<IActionResult> AddSkillToPosition(int positionId, int skillId)
        {
            AddSkillToPositionCommand command = new AddSkillToPositionCommand
            {
                PositionId = positionId,
                SkillId = skillId
            };

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(int id, UpdatePositionCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var result = await Mediator.Send(new DeletePositionCommand { Id = id });
            return Ok(result);
        }
    }
}
