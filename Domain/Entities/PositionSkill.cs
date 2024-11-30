using Domain.Common;

namespace Domain.Entities
{
    public class PositionSkill : AuditableBaseEntity
    {
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
