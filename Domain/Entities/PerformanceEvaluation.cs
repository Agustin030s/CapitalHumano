using Domain.Common;

namespace Domain.Entities
{
    public class PerformanceEvaluation : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EvaluationDate { get; set; }
        public string Evaluator { get; set; }
    }
}
