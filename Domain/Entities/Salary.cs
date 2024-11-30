using Domain.Common;

namespace Domain.Entities
{
    public class Salary : AuditableBaseEntity
    {
        private decimal _discountAmount;
        private decimal _netSalary;
        public DateTime PaymentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal DiscountAmount
        {
            get
            {
                if (this._discountAmount <= 0)
                {
                    this._discountAmount = this.GrossSalary * (this.DiscountPercentage / 100);
                }

                return this._discountAmount;
            }
            set
            {
                this._discountAmount = value;
            }
        }
        public decimal NetSalary
        {
            get
            {
                if (this._netSalary <= 0)
                {
                    this._netSalary = this.GrossSalary - this.DiscountAmount;
                }

                return this._netSalary;
            }
            set
            {
                this._netSalary = value;
            }
        }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
