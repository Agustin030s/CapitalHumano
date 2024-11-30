using Domain.Common;

namespace Domain.Entities
{
    public class Position : AuditableBaseEntity
    {
        public string Description { get; set; }
        public decimal GrossSalary { get; set; }
        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<PositionSkill> positionSkills { get; set; }
    }
}
