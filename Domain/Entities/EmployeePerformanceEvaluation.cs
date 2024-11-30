using Domain.Common;

namespace Domain.Entities
{
    public class EmployeePerformanceEvaluation : AuditableBaseEntity
    {
        public int Score { get; set; }
        public string? Feedback { get; set; }
        public string? EvaluationPeriod { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int EvaPerformanceId { get; set; }
        public PerformanceEvaluation PerformanceEvaluation { get; set; }
    }
}
