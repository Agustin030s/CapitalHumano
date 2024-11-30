using Domain.Enums;

namespace Application.DTOs
{
    public class PositionHistoryDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EndReason endReason { get; set; }
        public string? Observations { get; set; }
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
    }
}
