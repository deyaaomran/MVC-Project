using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
	public class Attendance : BaseEntity
	{
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }


    }
}
