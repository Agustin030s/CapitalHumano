using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Employee : AuditableBaseEntity
    {
        private int _age;
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public int Age
        {
            get
            {
                if(this._age <= 0)
                {
                    this._age = new DateTime(DateTime.Now.Subtract(this.Birthdate).Ticks).Year - 1;
                }
                return this._age;
            }
            set
            {
                this._age = value;
            }
        }
        public Genero Sexo { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
        public ICollection<EmployeeSkill> employeeSkills { get; set; }
        public ICollection<Salary> salaries { get; set; }
        public ICollection<PositionHistory> positionHistory { get; set; }
        public ICollection<EmployeeTraining> training { get; set; }
        public ICollection<EmployeePerformanceEvaluation> employeePerformanceEvaluations { get; set; }
    }
}
