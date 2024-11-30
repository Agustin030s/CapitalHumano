using Domain.Common;

namespace Domain.Entities
{
    public class Skill : AuditableBaseEntity
    {
        public string Description { get; set; }
    }
}
