using Domain.Common;

namespace Domain.Entities
{
    public class EmployeeSkill : AuditableBaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
