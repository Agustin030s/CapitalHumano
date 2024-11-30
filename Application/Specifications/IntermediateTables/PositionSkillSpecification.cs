using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.IntermediateTables
{
    public class PositionSkillSpecification : Specification<PositionSkill>
    {
        public PositionSkillSpecification(int positionId, int skillId)
        {
            Query.Where(ps => ps.PositionId == positionId && ps.SkillId == skillId);
        }
    }
}
