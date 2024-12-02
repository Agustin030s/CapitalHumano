using Application.Features.Skills.Commands.CreateSkillCommand;
using Application.Features.Skills.Commands.DeleteSkillCommand;
using Application.Features.Skills.Queries.GetAllSkillsQuery;
using Application.Features.Skills.Queries.GetSkillByIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace API_GCH.Controllers.v1
{
    public class SkillController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var result = await Mediator.Send(new GetAllSkillsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillById(int id)
        {
            var result = await Mediator.Send(new GetSkillByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(CreateSkillCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var result = await Mediator.Send(new DeleteSkillCommand { Id = id });
            return Ok(result);
        }
    }
}
