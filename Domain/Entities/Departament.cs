using Domain.Common;

namespace Domain.Entities
{
    public class Departament : AuditableBaseEntity
    {
        public string DepartamentCode { get; set; }
        public string Description { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}
