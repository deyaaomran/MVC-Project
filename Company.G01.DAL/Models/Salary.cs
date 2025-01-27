using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
	public class Salary : BaseEntity
	{
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
        public DateTime PayDate { get; set; } = DateTime.Now.Date;
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }

}
