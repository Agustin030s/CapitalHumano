using Domain.Common;

namespace Domain.Entities
{
    public class EmployeeTraining : AuditableBaseEntity
    {
        public DateTime CompletionDate { get; set; }
        public bool IsCertified { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
