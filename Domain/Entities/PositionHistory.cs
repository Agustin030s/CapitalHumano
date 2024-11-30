using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class PositionHistory : AuditableBaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EndReason? endReason { get; set; }
        public string? Observations { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
    }
}
