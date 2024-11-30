namespace Domain.Common
{
    public class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
