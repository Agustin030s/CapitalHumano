namespace Application.DTOs
{
    public class SalaryDto
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetSalary { get; set; }
        public int EmployeeId { get; set; }
    }
}
