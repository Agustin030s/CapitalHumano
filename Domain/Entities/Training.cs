using Domain.Common;

namespace Domain.Entities
{
    public class Training : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime TrainingDate { get; set; }
        public string Provider { get; set; }
    }
}
