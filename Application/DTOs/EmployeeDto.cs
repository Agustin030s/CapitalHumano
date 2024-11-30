using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public Genero Sexo { get; set; }
        public int PositionId { get; set; }
        public ICollection<EmployeeSkillDto> employeeSkills { get; set; }
        public ICollection<SalaryDto> salaries { get; set; }
        public ICollection<PositionHistoryDto> positionHistory { get; set; }
        public ICollection<EmployeeTrainingDto> training { get; set; }
        public ICollection<EmployeePerformanceEvaluationDto> employeePerformanceEvaluations { get; set; }
    }

    public class EmployeeSkillDto
    {
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
    }

    public class EmployeeTrainingDto
    {
        public DateTime CompletionDate { get; set; }
        public bool IsCertified { get; set; }
        public int EmployeeId { get; set; }
        public int TrainingId { get; set; }
    }

    public class EmployeePerformanceEvaluationDto
    {
        public int Score { get; set; }
        public string? Feedback { get; set; }
        public string? EvaluationPeriod { get; set; }
        public int EmployeeId { get; set; }
        public int EvaPerformanceId { get; set; }
    }
}
