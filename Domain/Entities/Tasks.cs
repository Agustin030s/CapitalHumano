using Domain.Common;

namespace Domain.Entities
{
    public class Tasks : AuditableBaseEntity
    {
        public string Description { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
