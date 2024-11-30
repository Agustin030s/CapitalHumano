namespace Application.DTOs
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int GrossSalary { get; set; }
        public int DepartamentId { get; set; }
        public ICollection<TaskDto> Tasks { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; }
        public ICollection<PositionsSkillDto> positionSkills { get; set; }
    }

    public class CreatePositionDto
    {
        public string Description { get; set; }
        public int GrossSalary { get; set; }
        public int DepartamentId { get; set; }
        public List<int>? SkillsId { get; set; }
        public List<string>? TaskDescriptions { get; set; }
    }
}
